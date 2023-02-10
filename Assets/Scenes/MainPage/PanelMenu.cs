using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelMenu : MonoBehaviour
{
    public GameObject panel;
 
     void Start () 
     { 
         panel.SetActive( false); 
     }
         
     public void PopUp (GameObject panel) 
     { 
         panel.SetActive (!panel.activeSelf); 
     }

     public void OnProfileClick() 
     { 
        SceneManager.LoadScene("UserProfile");
     }
    
    }

