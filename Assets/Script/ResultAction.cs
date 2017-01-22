using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultAction : MonoBehaviour {

    private Image lengthGauge;
    private float targetLengthGaugeWidth;
    private float lengthGaugeWidth;

    void Awake()
    {
        lengthGauge = this.transform.FindChild("Panel/Gauge_Value").GetComponent<Image>();
    }

	// Use this for initialization
	void Start () {
        targetLengthGaugeWidth = lengthGauge.rectTransform.sizeDelta.x * GameValueManager.SetGetUseCodeRatio;
        lengthGauge.fillAmount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        lengthGauge.fillAmount += lengthGauge.fillAmount < GameValueManager.SetGetUseCodeRatio ? 0.4f * Time.deltaTime : 0;
        if(lengthGauge.fillAmount >= GameValueManager.SetGetUseCodeRatio)
        {
            lengthGauge.fillAmount = GameValueManager.SetGetUseCodeRatio;
        }

    }
}
