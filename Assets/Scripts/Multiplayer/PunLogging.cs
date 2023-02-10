using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PunLogging : MonoBehaviour
{
    [SerializeField]
    Transform m_PunLogMsgContentTransform;
    [SerializeField]
    GameObject m_MsgPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLogMsg(string _msg)
    {
        
        TextMeshProUGUI tmp = Instantiate(m_MsgPrefab, m_PunLogMsgContentTransform).GetComponent<TextMeshProUGUI>();
        tmp.text = _msg;
    }
}
