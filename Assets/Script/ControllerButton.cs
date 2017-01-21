using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButton : MonoBehaviour {

    private Rigidbody2D playerRb;
    private bool pushRight = false;
    private bool pushLeft = false;
    private bool pushDown = false;

    void Awake()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pushRight) playerRb.AddForce(Vector2.right * GameValueManager.SetGetPlayerMovePower, ForceMode2D.Force);
        if (pushLeft) playerRb.AddForce(Vector2.left * GameValueManager.SetGetPlayerMovePower, ForceMode2D.Force);
        if (pushDown) playerRb.AddForce(Vector2.down * GameValueManager.SetGetPlayerMovePower, ForceMode2D.Force);
    }

    public void RightDown()
    {
        pushRight = true;
    }

    public void RightUp()
    {
        pushRight = false;
    }

    public void LeftDown()
    {
        pushLeft = true;
    }

    public void LeftUp()
    {
        pushLeft = false;
    }

    public void DownDown()
    {
        pushDown = true;
    }

    public void DownUp()
    {
        pushDown = false;
    }
}
