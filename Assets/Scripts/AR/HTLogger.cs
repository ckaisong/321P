using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;

public class HTLogger : MonoBehaviour
{
    [SerializeField]
    private GameObject m_textPrefab;

    [SerializeField]
    private Transform m_contentTransform;


    void AddLogMsg(VuforiaInitError error)
    {
        AddMsg(error.ToString());
    }
    public void AddMsg(string _text)
    {
        GameObject msg = Instantiate(m_textPrefab, m_contentTransform);
        TextMeshProUGUI tmp = msg.GetComponent<TextMeshProUGUI>();
        tmp.text = _text;
        //tmp.fontSize = 10;
    }
    // Start is called before the first frame update
    void Awake()
    {
        //AddMsg("HTLogger is awake!!");
        //VuforiaApplication.Instance.OnVuforiaInitialized += AddLogMsg;
    }

    // Update is called once per frame
    void Update()
    {
        //AddMsg(Camera.main.transform.eulerAngles.ToString());
    }
}
