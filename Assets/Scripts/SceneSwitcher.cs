using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    /*public void ChangeSceneAsync(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }*/
}
