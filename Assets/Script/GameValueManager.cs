using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValueManager : MonoBehaviour {

    public int maxLength;

    void Awake()
    {
        SetGetObjectiveMaxCodeLength = maxLength;
        SetGetIsPlayingGame = false;
    }

    public static int SetGetObjectiveMaxCodeLength { set; get; }
    public static bool SetGetIsPlayingGame { set; get; }
}
