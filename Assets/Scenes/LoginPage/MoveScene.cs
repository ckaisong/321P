using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public void RegisterPage(int sceneID)

    {
        SceneManager.LoadScene(sceneID);
    }


    public void LoginAsGuest(int sceneID)

    {
        SceneManager.LoadScene(sceneID);
    }

    public void LoginUser(int sceneID)

    {
        SceneManager.LoadScene(sceneID);
    }

    public void ShoppingCart(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }        

}
