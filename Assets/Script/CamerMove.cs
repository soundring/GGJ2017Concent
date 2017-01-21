using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMove : MonoBehaviour {
    
    [SerializeField, Range(0,1)]
    private float maxMoveClamp = 0.3f;
    private int clampedHeight;
    private GameObject player;
    private GameObject canvas;
    private Vector2 playerOldPos, playerNewPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("Canvas").gameObject;
    }

    // Use this for initialization
    void Start () {
        clampedHeight = (int)(Screen.height * maxMoveClamp);
        Debug.Log(clampedHeight);
    }
	
	// Update is called once per frame
	void Update () {
        playerNewPos = player.transform.position;
        var plsyerMoveMagni = (playerNewPos - playerOldPos).magnitude;
        var playerScreenSpace = Camera.main.WorldToScreenPoint(player.transform.position);
        if (playerScreenSpace.y < clampedHeight)
        {
            this.transform.position -= new Vector3(0, plsyerMoveMagni, 0);
        }
        playerOldPos = playerNewPos;
    }
}
