using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IPunInstantiateMagicCallback
{
    [SerializeField]
    [Tooltip("Player name display object")]
    TextMeshPro m_PlayerNameTMP;

    [SerializeField]
    [Tooltip("Player prefab's camera gameobject")]
    GameObject _myCamera;

    //To reference previous active camera (the default camera in the scene)
    GameObject _lastCamera;

    [SerializeField]
    PhotonVoiceView m_PhotonVoiceView;

    [SerializeField]
    //GameObject m_speakingIndicator;
    Animator m_indicatorAnim;

    float rotationSpeed= 1.0f;
    Vector3 m_OGforward;
    Vector3 m_LeftLimit;
    Vector3 m_RightLimit;

    /*PhotonView m_photonView;
    public PhotonView photonView
    {
        get
        {
            if (m_photonView == null)
                m_photonView = GetComponent<PhotonView>();
            return m_photonView;
        }
    }*/

    public void SetForward(Vector3 _forward)
    {
        m_OGforward= _forward;
        m_RightLimit = Quaternion.Euler(0f,45f,0f) * m_OGforward;
        m_LeftLimit = Quaternion.Euler(0f,-45f,0f) * m_OGforward;
    }

    public void SetName(string _newName)
    {
        m_PlayerNameTMP.text = _newName;
    }
    public void CameraOn()
    {
        if(_lastCamera == null)
        {
            _lastCamera = Camera.main.gameObject;
        }
        _lastCamera.SetActive(false);
        _myCamera.SetActive(true);
    }
    public void CameraOff()
    {
        _myCamera.SetActive(false);
        _lastCamera.SetActive(true);
    }

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        //Debug.Log(info.Sender.NickName);
        SetName(info.Sender.NickName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PhotonVoiceView.IsRecording)
        {
            //Debug.Log($"{PunVoiceClient.Instance.PrimaryRecorder.LevelMeter.CurrentAvgAmp} ");
            //m_speakingIndicator.SetActive(true);
            //m_indicatorAnim.SetTrigger("IsSpeaking");
            m_indicatorAnim.SetBool("Speaking", true);
        }
        else
        {
            //m_speakingIndicator.SetActive(false);
            m_indicatorAnim.SetBool("Speaking", false);
        }
        //Debug.Log($"accel:{Input.acceleration}");
        Vector3 inAccel = Quaternion.Euler(-90f,0f,0f) * Input.acceleration;
        //Debug.DrawRay(transform.position + Vector3.up, inAccel, Color.cyan);
        //transform.rotation *= Quaternion.Euler(0f, inAccel.x * rotationSpeed, 0f);
    }
}
