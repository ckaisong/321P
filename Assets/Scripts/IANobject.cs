using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IANobject : MonoBehaviour
{
    float _duration;

    public float Duration { set
        {
            _duration= value;
        }
    }

    public void Activate()
    {
        StartCoroutine(DestroyAfterTime());
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(_duration);
        Destroy(gameObject);
        InAppNotification.Instance.GoNext();
    }
}
