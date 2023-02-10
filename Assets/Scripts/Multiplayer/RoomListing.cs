using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    Transform m_RoomListTransform;

    [SerializeField]
    GameObject m_RoomButtonPrefab;

    private Dictionary<string, GameObject> m_rmList = new Dictionary<string, GameObject>();

    /* Adding of new room to displayed list
     * Parameters:
     *      -_rmInfo: the new room to be added
     */
    public GameObject AddRoom(RoomInfo _rmInfo)
    {
        int currSize = m_rmList.Count;
        if (m_rmList.ContainsKey(_rmInfo.Name))
            return null;
        if (currSize == 0 || currSize == m_RoomListTransform.childCount)
        {
            //instantiate prefab
            GameObject rm = Instantiate(m_RoomButtonPrefab, m_RoomListTransform);
            /*//set button's text to room name
            rm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _rmInfo.Name;
            //Add on click event 
            rm.GetComponent<Button>().onClick.AddListener(() => {
                PhotonNetwork.JoinRoom(_rmInfo.Name);
                ClearAllChildObjects();
            });*/
            //add to list
            m_rmList.Add(_rmInfo.Name, rm);
            return rm;
        }
        else
        {
            //instantiate prefab
            GameObject rm = m_RoomListTransform.GetChild(currSize - 1).gameObject;
            rm.SetActive(true);
            //add to list
            m_rmList.Add(_rmInfo.Name, rm);
            return rm;
        }
    }
    /* Removing room from displayed list when removed from photon list
     * Parameters:
     *      -_rmInfo: the room being removed
     */
    public void RemoveRoom(RoomInfo _rmInfo)
    {
        if (!m_rmList.ContainsKey(_rmInfo.Name))
            return;
        //Destroy the button gameobject
        Destroy(m_rmList[_rmInfo.Name]);
        //remove from list
        m_rmList.Remove(_rmInfo.Name);
    }

    /* For clearing of list of displayed rooms
     */
    public IEnumerator ClearAllChildObjects()
    {
        //loop destroy first child until no more child objects
        while(m_RoomListTransform.childCount > 0)
        {
            Destroy(m_RoomListTransform.GetChild(0));
            yield return new WaitForEndOfFrame();
        }
        //clear dictionary
        m_rmList.Clear();
    }
    public void ClearAll()
    {
        //m_PlayerListSV.Clear();
        for (int i = 0; i < m_RoomListTransform.childCount; ++i)
        {
            m_RoomListTransform.GetChild(i).gameObject.SetActive(false);
        }
        m_rmList.Clear();
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
