using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeLengthLimit : MonoBehaviour {

    private Text limitText;
    private CodeJoint rootCodeJoint;

    void Awake()
    {
        limitText = this.GetComponent<Text>();
        rootCodeJoint = GameObject.Find("RootCodeParts").GetComponent<CodeJoint>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        limitText.text = (GameValueManager.SetGetObjectiveMaxCodeLength - rootCodeJoint.GetUseCodeLength) + "m";
    }
}
