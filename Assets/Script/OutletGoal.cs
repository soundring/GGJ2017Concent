using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OutletGoal : MonoBehaviour
{
    private GameObject playerObj;
    private Animator playerAnimator;
    private RawImage lightArea;
    private Image flash;
    private float a = 200;
    private AudioSource audioSource;
    public AudioClip connectSe;
    public AudioClip lightSe;
    private bool isSePlay = false;

    void Awake()
    {
        lightArea = GameObject.Find("LightArea").GetComponent<RawImage>();
        flash = GameObject.Find("Flash").GetComponent<Image>();
        audioSource = this.GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        flash.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerObj = other.gameObject;
            playerObj.GetComponent<CircleCollider2D>().enabled = false;
            playerAnimator = playerObj.GetComponent<Animator>();
            GameValueManager.SetGetIsPlayingGame = false;
            StartCoroutine("ConnectingAction");
        }
    }

    IEnumerator ConnectingAction()
    {
        int loopCount = 0;
        Vector3 constPos;
        //ズームアップ
        while (loopCount < 60)
        {
            var nowPlayerPos = playerObj.transform.position;
            var targetPlayerPos = new Vector3(this.transform.position.x, this.transform.position.y + 1.8f, playerObj.transform.position.z);
            playerObj.transform.position = Vector3.Lerp(nowPlayerPos, targetPlayerPos, 0.3f);

            //var nowPlayerAng = playerObj.transform.eulerAngles;
            //var targetPlayerAng = Quaternion.LookRotation(this.transform.position - playerObj.transform.position).eulerAngles;
            //targetPlayerAng = new Vector3(0, 0, targetPlayerAng.z);
            //playerObj.transform.eulerAngles = Vector3.Lerp(nowPlayerAng, targetPlayerAng, 0.3f);
            playerAnimator.speed = 0;
            var nowCamPos = Camera.main.transform.position;
            var targetCamPos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -5);
            Camera.main.transform.position = Vector3.Lerp(nowCamPos, targetCamPos, 0.3f);
            loopCount++;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //差し込む
        constPos = playerObj.transform.position;
        loopCount = 0;
        while (loopCount < 30)
        {
            if(!isSePlay)
            {
                audioSource.PlayOneShot(connectSe);
                isSePlay = true;
            }
            //var nowPlayerPos = playerObj.transform.position;
            //var targetPlayerPos = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, playerObj.transform.position.z);
            playerObj.transform.position = constPos;

            //var nowPlayerAng = playerObj.transform.eulerAngles;
            //var targetPlayerAng = Quaternion.LookRotation(this.transform.position - playerObj.transform.position).eulerAngles;
            //targetPlayerAng = new Vector3(0, 0, targetPlayerAng.z);
            //playerObj.transform.eulerAngles = Vector3.Lerp(nowPlayerAng, targetPlayerAng, 0.3f);
            playerAnimator.speed = 1;
            playerAnimator.SetBool("connectFlag", true);

            var nowCamPos = Camera.main.transform.position;
            var targetCamPos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -5);
            Camera.main.transform.position = Vector3.Lerp(nowCamPos, targetCamPos, 0.3f);
            loopCount++;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //ズームダウン
        loopCount = 0;
        isSePlay = false;
        while (loopCount < 60)
        {
            if (!isSePlay)
            {
                audioSource.PlayOneShot(lightSe);
                isSePlay = true;
            }
            var nowSize = lightArea.rectTransform.sizeDelta;
            var targetCamSize = new Vector2(100000, 100000);
            lightArea.rectTransform.sizeDelta = Vector3.Lerp(nowSize, targetCamSize, 0.05f);
            var nowCamPos = Camera.main.transform.position;
            var targetCamPos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -10);
            Camera.main.transform.position = Vector3.Lerp(nowCamPos, targetCamPos, 0.1f);
            playerObj.transform.position = constPos;
            a = Mathf.Lerp(a, 0, 0.03f);
            flash.color = new Color(1, 1, 1, a / 255);
            loopCount++;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //リザルトをaddScene
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Result", LoadSceneMode.Additive);
        SoundController.ChangeBGM(Resources.Load("BGM/crear") as AudioClip, false);
        StopCoroutine("ConnectingAction");
    }
}
