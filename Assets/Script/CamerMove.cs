using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour {
    
    [SerializeField, Range(0,1)]
    private float maxMoveClamp = 0.3f;
    private int clampedHeight;
    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start () {
        clampedHeight = (int)((float)Screen.currentResolution.height * maxMoveClamp);
    }
	
	// Update is called once per frame
	void Update () {
        var screenPointOfPlayer = RectTransformUtility.WorldToScreenPoint(Camera.main, player.transform.position);
        if(screenPointOfPlayer.y < clampedHeight)
        {
            var diffY = Mathf.Abs(screenPointOfPlayer.y - clampedHeight);
            var newPosY = this.transform.position.y - diffY;
            this.transform.position = new Vector3(0, newPosY, -10);
            Debug.Log("screenPointOfPlayer = " + screenPointOfPlayer);
            Debug.Log("clampedHeight = " + clampedHeight);
            Debug.Log("diffY = " + diffY);
        }

    }
}
