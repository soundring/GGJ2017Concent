using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutletGoal : MonoBehaviour
{
    private GameObject playerObj;
    private RawImage lightArea;
    private Image flash;
    private float a = 200;

    void Awake()
    {
        lightArea = GameObject.Find("LightArea").GetComponent<RawImage>();
        flash = GameObject.Find("Flash").GetComponent<Image>();
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
            playerObj.GetComponent<BoxCollider2D>().enabled = false;
            GameValueManager.SetGetIsPlayingGame = false;
            StartCoroutine("ConnectingAction");
        }
    }

    IEnumerator ConnectingAction()
    {
        int loopCount = 0;
        Vector3 constPos;
        while (loopCount < 60)
        {
            var nowPlayerPos = playerObj.transform.position;
            var targetPlayerPos = new Vector3(this.transform.position.x, this.transform.position.y + 0.9f, playerObj.transform.position.z);
            playerObj.transform.position = Vector3.Lerp(nowPlayerPos, targetPlayerPos, 0.3f);

            var nowPlayerAng = playerObj.transform.eulerAngles;
            var targetPlayerAng = Quaternion.LookRotation(this.transform.position - playerObj.transform.position).eulerAngles;
            targetPlayerAng = new Vector3(0, 0, targetPlayerAng.z);
            playerObj.transform.eulerAngles = Vector3.Lerp(nowPlayerAng, targetPlayerAng, 0.3f);

            var nowCamPos = Camera.main.transform.position;
            var targetCamPos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -5);
            Camera.main.transform.position = Vector3.Lerp(nowCamPos, targetCamPos, 0.3f);
            loopCount++;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        loopCount = 0;
        while (loopCount < 30)
        {
            var nowPlayerPos = playerObj.transform.position;
            var targetPlayerPos = new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, playerObj.transform.position.z);
            playerObj.transform.position = Vector3.Lerp(nowPlayerPos, targetPlayerPos, 0.3f);

            var nowPlayerAng = playerObj.transform.eulerAngles;
            var targetPlayerAng = Quaternion.LookRotation(this.transform.position - playerObj.transform.position).eulerAngles;
            targetPlayerAng = new Vector3(0, 0, targetPlayerAng.z);
            playerObj.transform.eulerAngles = Vector3.Lerp(nowPlayerAng, targetPlayerAng, 0.3f);

            var nowCamPos = Camera.main.transform.position;
            var targetCamPos = new Vector3(playerObj.transform.position.x, playerObj.transform.position.y, -5);
            Camera.main.transform.position = Vector3.Lerp(nowCamPos, targetCamPos, 0.3f);
            loopCount++;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        constPos = playerObj.transform.position;
        loopCount = 0;
        while (loopCount < 60)
        {
            var nowSize = lightArea.rectTransform.sizeDelta;
            var targetCamSize = new Vector2(5000, 5000);
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
        //TODO:リザルトをaddScene
    }
}
