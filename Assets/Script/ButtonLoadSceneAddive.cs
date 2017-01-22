using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadSceneAddive : MonoBehaviour {

    public void TransitionAdditive(string sceneName)
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}
