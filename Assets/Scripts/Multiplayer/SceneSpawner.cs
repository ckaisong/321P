using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneSpawner : MonoBehaviour
{
    [SerializeField]
    /*GameObject*/string m_prefabPlayer;

    [SerializeField]
    GameObject m_HostStage;

    [SerializeField]
    GameObject[] m_ParticipantsStage;

    PlayerScript m_PSme;
    public void AddAsHost(string _playerName)
    {
        PlayerScript goPS = SpawnPlayer(m_HostStage.transform);
        //goPS.SetName(_playerName);
        goPS.SetForward(goPS.transform.forward);
        goPS.CameraOn();
        m_PSme = goPS;
    }

    public void AddAsParticipant(string _playerName)
    {
        PlayerScript goPS = SpawnPlayer(m_ParticipantsStage[PhotonNetwork.CurrentRoom.PlayerCount-2].transform);
        goPS.transform.forward = m_HostStage.transform.position - m_ParticipantsStage[PhotonNetwork.CurrentRoom.PlayerCount - 2].transform.position;
        //goPS.SetName(_playerName);
        goPS.SetForward(goPS.transform.forward);
        goPS.CameraOn();
        m_PSme = goPS;
    }

    private PlayerScript SpawnPlayer(Transform _parentTransform)
    {
        //Use Photon to initialize player prefab
        GameObject go = PhotonNetwork.Instantiate(m_prefabPlayer, _parentTransform.position + new Vector3(0,0.2f,0f), _parentTransform.rotation);
        //go.transform.forward = m_HostStage.transform.position - _parentTransform.position;
        //retun the PlayerScript attached
        return go.GetComponent<PlayerScript>();
    }

    public void RemovePlayer()
    {
        m_PSme.CameraOff();
        PhotonNetwork.Destroy(m_PSme.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
