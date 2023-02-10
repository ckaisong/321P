using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingCardCanvas : MonoBehaviour
{
    public static GreetingCardCanvas Instance;
    
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
