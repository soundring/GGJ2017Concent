using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeJoint : MonoBehaviour {

    private HingeJoint2D joint;
    private const float limitPower = 50;
    [SerializeField]
    private HingeJoint2D nextConnectParts;
    private GameObject prefabCodeParts;
    private int codeLength;
    private Text countDownText;

    void Awake()
    {
        joint = this.GetComponent<HingeJoint2D>();
        prefabCodeParts = Resources.Load("Prefab/CodeParts") as GameObject;
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {

        StartCoroutine("StartCountDOwn");
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
        countDownText.text = count.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1.1f);
            count--;
            if (count > 0)
            {
                countDownText.text = count.ToString();
            }
            else if (count <= 0)
            {
                countDownText.text = "はじめ!";
                yield return new WaitForSeconds(1f);
                countDownText.text = "";
                StopCoroutine("StartCountDOwn");
                GameValueManager.SetGetIsPlayingGame = true;
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
                    newJoint.transform.localPosition = new Vector3(0, -0.618f, 0);
                    newJoint.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newJointComp.connectedAnchor = new Vector2(0, -0.075f);
                    newJoint.GetComponent<Rigidbody2D>().simulated = true;
                    newJointComp.connectedBody = this.GetComponent<Rigidbody2D>();
                    nextConnectParts.connectedBody = newJoint.GetComponent<Rigidbody2D>();
                    nextConnectParts = newJointComp;
                    codeLength++;
                }
                else
                {
                    StartCoroutine("CountDown");
                    StopCoroutine("CreateJoint");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// コード最大延長後のカウントダウン
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3f);
        int count = 3;
        countDownText.text = count.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            count--;
            if(count > 0)
            {
                countDownText.text = count.ToString();
            }
            else if(count <= 0)
            {
                countDownText.text = "そこまで!";
                yield return new WaitForSeconds(3f);
                countDownText.text = "";
                StopCoroutine("CountDown");
                GameValueManager.SetGetIsPlayingGame = false;
                break;
            }
        }
    }

    public int GetUseCodeLength{ get { return codeLength; } }
}
