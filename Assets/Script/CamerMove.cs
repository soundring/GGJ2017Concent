using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour {
    
    [SerializeField, Range(0,1)]
    private float maxMoveClamp = 0.4f;
    private int clampedHeight;
    private GameObject player;
    private float diffY;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () {
        clampedHeight = (int)((float)Screen.currentResolution.height * maxMoveClamp);
        var c = RectTransformUtility.WorldToScreenPoint(Camera.main, Vector3.zero);
        diffY = Mathf.Abs(clampedHeight - c.y);
        Debug.Log("clampedHeight = " + clampedHeight);
    }
	
	// Update is called once per frame
	void Update () {
        var screenPointOfPlayer = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        var newPosY = screenPointOfPlayer.y < clampedHeight ? player.transform.position.y - diffY : this.transform.position.y;
        Debug.Log("screenPointOfPlayer.y = " + screenPointOfPlayer.y);
        //this.transform.position = new Vector3(0, newPosY, -10);

    }
}
