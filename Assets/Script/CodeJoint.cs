﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeJoint : MonoBehaviour {

    private HingeJoint2D joint;
    private const float limitPower = 50;
    [SerializeField]
    private HingeJoint2D nextConnectParts;
    private GameObject prefabCodeParts;
    private int codeLength;

    void Awake()
    {
        joint = this.GetComponent<HingeJoint2D>();
        prefabCodeParts = Resources.Load("Prefab/CodeParts") as GameObject;
    }

	// Use this for initialization
	void Start () {

        StartCoroutine("CreateJoint");
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// コードの延長
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateJoint()
    {
        while(true)
        {
            if (joint.reactionForce.magnitude > limitPower)
            {
                if (codeLength < GameValueManager.SetGetObjectiveMaxCodeLength)
                {
                    GameObject newJoint = Instantiate(prefabCodeParts) as GameObject;
                    HingeJoint2D newJointComp = newJoint.GetComponent<HingeJoint2D>();
                    newJoint.transform.SetParent(this.transform.parent);
                    newJoint.transform.localPosition = new Vector3(0, -0.618f, 0);
                    newJoint.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newJointComp.connectedAnchor = new Vector2(0, -0.075f);
                    newJoint.GetComponent<Rigidbody2D>().simulated = true;
                    newJointComp.connectedBody = this.GetComponent<Rigidbody2D>();
                    nextConnectParts.connectedBody = newJoint.GetComponent<Rigidbody2D>();
                    nextConnectParts = newJointComp;
                    codeLength++;
                }
                else
                {
                    StartCoroutine("CountDown");
                    StopCoroutine("CreateJoint");
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// コード最大延長後のカウントダウン
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            yield return new WaitForSeconds(1f);
        }
    }

    public int GetUseCodeLength{ get { return codeLength; } }
}
