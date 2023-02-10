using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class OnSelectScene0 : MonoBehaviour
{
    
    public void HandleInputData(string str)
    {
        if (str == "Coffee")
        {          
            SceneManager.LoadScene(11, LoadSceneMode.Additive);         
        }
         
        //if (str != "Coffee" && str != "Staffed Toybear" )
        //{
        //    SceneManager.LoadScene(11); 
        //}

        if (str == "Staffed Toybear")
        {
            SceneManager.LoadScene(12);
        }
    }
}
//LoadSceneMode.Additive