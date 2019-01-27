using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartApplication : MonoBehaviour {

    public void StartGame() {
        var globalScripts = GameObject.Find("GlobalScripts");
        if (globalScripts != null) Destroy(globalScripts);
        LoadByIndex(1);
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
