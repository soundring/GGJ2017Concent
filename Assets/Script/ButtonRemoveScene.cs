using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonRemoveScene : MonoBehaviour {

    public void UnloadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(sceneName);
        Resources.UnloadUnusedAssets();
    }
}
