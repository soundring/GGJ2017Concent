using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CodeJoint : MonoBehaviour {

    private HingeJoint2D joint;
    private const float limitPower = 50;
    [SerializeField]
    private HingeJoint2D nextConnectParts;
    private GameObject prefabCodeParts;
    private int codeLength;
    private Image countDownImage;
    private Animation CountDownAnim;
    private Sprite[] countImageList;
    private Sprite startSprite;
    private Sprite failureSprite;
    private Image statusImage;


    void Awake()
    {
        joint = this.GetComponent<HingeJoint2D>();
        prefabCodeParts = Resources.Load("Prefab/CodeParts") as GameObject;
        countDownImage = GameObject.Find("CountDownImage").GetComponent<Image>();
        CountDownAnim = GameObject.Find("CountDownImage").GetComponent<Animation>();
        countImageList = new Sprite[3];
        countImageList[0] = Resources.Load<Sprite>("Image/1");
        countImageList[1] = Resources.Load<Sprite>("Image/2");
        countImageList[2] = Resources.Load<Sprite>("Image/3");
        startSprite = Resources.Load("Image/start") as Sprite;
        failureSprite = Resources.Load<Sprite>("Image/failure");
        statusImage = GameObject.Find("GameStatusImage").GetComponent<Image>();
        Debug.Log(countImageList[0]);
    }

	// Use this for initialization
	void Start () {
        StartCoroutine("StartCountDOwn");
        statusImage.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// ゲーム開始カウントダウン
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartCountDOwn()
    {

        int count = 3;
        countDownImage.sprite = countImageList[count - 1];
        while (true)
        {
            yield return new WaitForSeconds(1.1f);
            count--;
            if (count > 0)
            {
                //CountDownAnim.Play();
                countDownImage.sprite = countImageList[count - 1];
            }
            else if (count <= 0)
            {
                countDownImage.enabled = false;
                statusImage.enabled = true;
                yield return new WaitForSeconds(1f);
                statusImage.enabled = false;
                StopCoroutine("StartCountDOwn");
                GameValueManager.SetGetIsPlayingGame = true;
                GameValueManager.SetGetIsStarted = true;
                break;
            }
        }
        StartCoroutine("CreateJoint");
    }

    /// <summary>
    /// コードの延長
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateJoint()
    {
        while(true)
        {
            if (joint.reactionForce.magnitude > limitPower)
            {
                if (codeLength < GameValueManager.SetGetObjectiveMaxCodeLength)
                {
                    GameObject newJoint = Instantiate(prefabCodeParts) as GameObject;
                    HingeJoint2D newJointComp = newJoint.GetComponent<HingeJoint2D>();
                    newJoint.transform.SetParent(this.transform.parent);
                    newJoint.transform.localPosition = new Vector3(0, -0.089f, 0);
                    newJoint.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newJointComp.connectedAnchor = new Vector2(0, -3.766256f);
                    newJoint.GetComponent<Rigidbody2D>().simulated = true;
                    newJointComp.connectedBody = this.GetComponent<Rigidbody2D>();
                    nextConnectParts.connectedBody = newJoint.GetComponent<Rigidbody2D>();
                    nextConnectParts = newJointComp;
                    codeLength++;
                    GameValueManager.SetGetUseCodeRatio = (float)codeLength / (float)GameValueManager.SetGetObjectiveMaxCodeLength;
                }
                else
                {
                    if(GameValueManager.SetGetIsPlayingGame)
                    {
                        StartCoroutine("CountDown");
                    }
                    StopCoroutine("CreateJoint");
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// コード最大延長後のカウントダウン
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3f);
        countDownImage.enabled = true;
        //statusImage.sprite = failureSprite;
        int count = 3;
        countDownImage.sprite = countImageList[count - 1];
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            count--;
            if(count > 0)
            {
                countDownImage.sprite = countImageList[count - 1];
            }
            else if(count <= 0)
            {
                countDownImage.enabled = false;
                //statusImage.enabled = true;
                //yield return new WaitForSeconds(3f);
                //statusImage.enabled = false;
                SceneManager.LoadScene("Failure", LoadSceneMode.Additive);
                StopCoroutine("CountDown");
                GameValueManager.SetGetIsPlayingGame = false;
                break;
            }
        }
    }

    public int GetUseCodeLength{ get { return codeLength; } }
}
