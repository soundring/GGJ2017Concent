using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Rigidbody2D rb;
    private float power = 200;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
        GameValueManager.SetGetPlayerMovePower = power;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //自動で下へ移動
        rb.AddForce(Vector2.down * GameValueManager.SetGetPlayerMovePower / 2, ForceMode2D.Force);

        //デバッグ用
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    rb.AddForce(Vector2.up * GameValueManager.SetGetPlayerMovePower, ForceMode2D.Force);
        //}
        //else if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    rb.AddForce(Vector2.down * GameValueManager.SetGetPlayerMovePower, ForceMode2D.Force);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rb.AddForce(Vector2.right * GameValueManager.SetGetPlayerMovePower, ForceMode2D.Force);
        //}
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rb.AddForce(Vector2.left * GameValueManager.SetGetPlayerMovePower, ForceMode2D.Force);
        //}
    }
}
