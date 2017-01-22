using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultAction : MonoBehaviour {

    private Image lengthGauge;
    private float targetLengthGaugeWidth;
    private float lengthGaugeWidth;
    private Image[] rankMarkList;
    private Sprite onRankSprite;

    void Awake()
    {
        lengthGauge = this.transform.FindChild("Panel/Gauge_Value").GetComponent<Image>();
        rankMarkList = new Image[3];
        rankMarkList[0] = this.transform.FindChild("Panel/Rank1").gameObject.GetComponent<Image>();
        rankMarkList[1] = this.transform.FindChild("Panel/Rank2").gameObject.GetComponent<Image>();
        rankMarkList[2] = this.transform.FindChild("Panel/Rank3").gameObject.GetComponent<Image>();
        onRankSprite = Resources.Load<Sprite>("Image/crear_02");
    }

	// Use this for initialization
	void Start () {
        targetLengthGaugeWidth = lengthGauge.rectTransform.sizeDelta.x * GameValueManager.SetGetUseCodeRatio;
        lengthGauge.fillAmount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        lengthGauge.fillAmount += lengthGauge.fillAmount < GameValueManager.SetGetUseCodeRatio ? 0.4f * Time.deltaTime : 0;
        if(lengthGauge.fillAmount > 0.5f)
        {
            rankMarkList[0].sprite = onRankSprite;
        }
        if (lengthGauge.fillAmount > 0.7f)
        {
            rankMarkList[1].sprite = onRankSprite;
        }
        if (lengthGauge.fillAmount > 0.9f)
        {
            rankMarkList[2].sprite = onRankSprite;
        }
        if (lengthGauge.fillAmount >= GameValueManager.SetGetUseCodeRatio)
        {
            lengthGauge.fillAmount = GameValueManager.SetGetUseCodeRatio;
        }

    }
}
