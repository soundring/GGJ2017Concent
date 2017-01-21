using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValueManager : MonoBehaviour {

    public int maxLength;

    void Awake()
    {
        SetGetObjectiveMaxCodeLength = maxLength;
    }

    public static int SetGetObjectiveMaxCodeLength { set; get; }
}
