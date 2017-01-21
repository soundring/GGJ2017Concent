using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    private Rigidbody2D rb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float power = 100;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * power, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(Vector2.down * power, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(Vector2.right * power, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector2.left * power, ForceMode2D.Force);
        }
    }
}
