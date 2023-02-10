using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Left panel's animation controller")]
    Animator LP_anim;
    bool LP_Open = false;

    [SerializeField]
    [Tooltip("Right panel's animation controller")]
    Animator RP_anim;
    bool RP_Open = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LPTrigger()
    {
        if(!LP_Open)
        {
            LP_Open = true;
            LP_anim.SetTrigger("open");
        }
        else
        {
            LP_Open = false;
            LP_anim.SetTrigger("close");
        }
    }

    public void RPTrigger()
    {
        if (!RP_Open)
        {
            RP_Open = true;
            RP_anim.SetTrigger("open");
        }
        else
        {
            RP_Open = false;
            RP_anim.SetTrigger("close");
        }
    }

    /*public void LPOpen()
    {
        LP_anim.SetTrigger("open");
    }
    public void LPClose()
    {
        LP_anim.SetTrigger("close");
    }

    public void RPOpen()
    {
        RP_anim.SetTrigger("open");
    }
    public void RPClose()
    {
        RP_anim.SetTrigger("close");
    }*/
}
