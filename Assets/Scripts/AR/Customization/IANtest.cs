using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANtest : MonoBehaviour
{
    public void log(string _msg)
    {
        InAppNotification.Instance.CreateNotif(_msg, 3.0f);
    }
}
