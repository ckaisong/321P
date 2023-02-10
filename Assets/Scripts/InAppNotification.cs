using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Singleton/InAppNotif")]
public class InAppNotification : MonoBehaviourSingleton<InAppNotification>
{
    [SerializeField]
    GameObject m_NotifPrefab;
    private Queue<IANobject> m_NotifQueue = new Queue<IANobject>();
    private IANobject currIAN;
    public void CreateNotif(string _msg, float _duration)
    {
        GameObject notif = Instantiate(m_NotifPrefab);
        TextMeshProUGUI tmp = notif.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmp.text = _msg;
        IANobject ian = notif.transform.GetComponent<IANobject>();
        ian.Duration= _duration;
        m_NotifQueue.Enqueue(ian);
        if(currIAN == null)
            GoNext();
    }
    public void CreateNotif(string _msg)
    {
        CreateNotif(_msg, 3.0f);
    }
    public void GoNext()
    {
        if (m_NotifQueue.Count < 1)
            return;
        currIAN = m_NotifQueue.Dequeue();
        currIAN.Activate();
    }
}
