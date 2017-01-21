using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightAreaMove : MonoBehaviour {

    private GameObject player;
    private RawImage image;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        image = this.GetComponent<RawImage>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector2 newPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        Resolution windowRes = Screen.currentResolution;
        float clampX = Mathf.Clamp(newPos.x, -windowRes.width, windowRes.width);
        float clampY = Mathf.Clamp(newPos.y, -windowRes.height, windowRes.height);
        newPos = new Vector2(clampX, clampY);
        image.rectTransform.position = newPos;
    }
}
