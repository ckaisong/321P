using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterPage : MonoBehaviour
{
    // Start is called before the first frame update
   public void BackToMainMenu(int sceneID)

    {
        SceneManager.LoadScene(sceneID);
    }

}
