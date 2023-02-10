using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonLaunch : MonoBehaviourPunCallbacks
{
    [SerializeField]
    [Tooltip("For logging stuff for now")]
    PunLogging m_PunLogger;

    [Header("General")]
    [SerializeField]
    [Tooltip("The panel that holds the intro layout")]
    GameObject m_IntroPanel;
    [SerializeField]
    [Tooltip("The input field where user input their nickname for Photon room")]
    TMP_InputField m_NicknameInput;
    [SerializeField]
    [Tooltip("The button that connects the player to Photon server and lists room")]
    Button m_CountinueButton;


    [Header("Room")]
    [SerializeField]
    [Tooltip("The script attached to the gameobject for displaying rooms")]
    RoomListing m_RoomListing;
    [SerializeField]
    [Tooltip("For setting visability of panel")]
    GameObject m_RoomPanel;
    [SerializeField]
    [Tooltip("For displaying name of room player is in on UI")]
    TextMeshProUGUI m_TMPRoomName;

    [Header("Player")]
    [SerializeField]
    [Tooltip("The script attached to the gameobject for displaying players in the room")]
    PlayerListing m_PlayerListing;
    [SerializeField]
    [Tooltip("For setting visability of panel")]
    GameObject m_PlayerPanel;
    /*[SerializeField]
    GameObject m_PlayerPrefabModel;*/
    [SerializeField]
    [Tooltip("For displaying player name on ui")]
    TextMeshProUGUI m_TMPPlayerName;
    [SerializeField]
    [Tooltip("SceneSpawner script to spawn new players")]
    SceneSpawner m_SPTheScene;

    bool m_AmRoomOwner = false;

    enum RoomState
    {
        RS_Enter,
        RS_RoomSelect,
        RS_InRoom
    }
    RoomState m_RScurr = RoomState.RS_Enter;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        m_PunLogger.AddLogMsg("Connecting to Photon");
        PhotonNetwork.ConnectUsingSettings();
        m_IntroPanel.SetActive(true);
        m_RoomPanel.SetActive(false);
        m_PlayerPanel.SetActive(false);
        m_CountinueButton.onClick.AddListener(() => {
            string nickname = m_NicknameInput.textComponent.text;
            PhotonNetwork.NickName = nickname;
            m_TMPPlayerName.text = nickname;
            m_PunLogger.AddLogMsg("Joining lobby");
            m_RScurr = RoomState.RS_RoomSelect;
            PhotonNetwork.JoinLobby();
        });
    }
    #region Connecting to photon
    public override void OnConnectedToMaster()
    {
        m_PunLogger.AddLogMsg("Connected to Photon");
        if(m_RScurr == RoomState.RS_RoomSelect)
        {
            PhotonNetwork.JoinLobby();
        }
        /*m_PunLogger.AddLogMsg("Joining lobby");*/
        //Debug.Log($"{System.DateTime.Now.Millisecond}{System.DateTime.Now.Millisecond}");
        //Setting this player's nickname
        /*if(PhotonNetwork.NickName == ""){
            *//*System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            int val = (int)(System.DateTime.UtcNow - dt).TotalMilliseconds;*/
            /*****************************
             **** To change in future ****
             *****************************//*
            //Using Ticks for random number to set as player's name
            int val = (int)System.DateTime.Now.Ticks;
            //Sets player nickname
            PhotonNetwork.NickName = "Player " + val.ToString();
            m_PunLogger.AddLogMsg($"Player name set: {PhotonNetwork.NickName}");
            //Set display text to nickname above
            m_TMPPlayerName.text = PhotonNetwork.NickName;
        }
        PhotonNetwork.JoinLobby();*/
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        m_PunLogger.AddLogMsg($"Disconnected from server: {cause}");
        //If player timeouts from server, reconnect automatically
        if (cause == DisconnectCause.ServerTimeout)
        { 
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    #endregion
    
    #region Lobby related events
    public override void OnJoinedLobby()
    {
        m_PunLogger.AddLogMsg($"Joined lobby: {PhotonNetwork.CurrentLobby}");
        //Switching panel visibility
        /**********************************
         * To be replaced when UI is done *
         **********************************/
        m_IntroPanel.SetActive(false);
        m_PlayerPanel.SetActive(false);
        m_RoomPanel.SetActive(true);
    }
    #endregion

    #region Room related events
    public override void OnCreatedRoom()
    {
        m_PunLogger.AddLogMsg($"Room successfully created");
        m_AmRoomOwner = true;
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        m_PunLogger.AddLogMsg($"Room creation failed: {returnCode}-'{message}'");
    }
    public override void OnJoinedRoom()
    {
        m_PunLogger.AddLogMsg($"Joined room: {PhotonNetwork.CurrentRoom.Name}");
        //StartCoroutine(m_RoomListing.ClearAllChildObjects());
        //m_RoomListing.ClearAll();
        //If this player is first to join the room, spawn him at host stage
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            m_SPTheScene.AddAsHost(PhotonNetwork.NickName);
        }
        //else, spawn him at participant stage
        else
        {
            m_SPTheScene.AddAsParticipant(PhotonNetwork.NickName);
        }
        //initialise players in the room
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            m_PlayerListing.AddPlayer(player);
            /*if (player == PhotonNetwork.LocalPlayer)
            {
                //m_PunLogger.AddLogMsg($"It's a me {player.NickName}!");

            }*/
        }
        //set room name display text to current room name
        m_TMPRoomName.text = PhotonNetwork.CurrentRoom.Name;
        m_RScurr = RoomState.RS_InRoom;
        //switch panel visibility
        m_RoomPanel.SetActive(false);
        m_PlayerPanel.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        m_PunLogger.AddLogMsg($"Left Room ()");
        //StartCoroutine(m_PlayerListing.ClearAllChildObjects());
        //clear player listing display
        m_PlayerListing.ClearAll();
        //remove this player's object from scene
        m_SPTheScene.RemovePlayer();
        m_RScurr = RoomState.RS_RoomSelect;
        //switch panel visibility
        m_RoomPanel.SetActive(true);
        m_PlayerPanel.SetActive(false);
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo room in roomList)
        {
            //check if room is removed from list
            if(room.RemovedFromList)
            {
                m_RoomListing.RemoveRoom(room);
            }
            //At this point, room is added to list
            else
            {
                m_PunLogger.AddLogMsg($"Adding Room: {room}");
                //get gameobject reference from room listing script
                GameObject rm = m_RoomListing.AddRoom(room);
                //if room doesn't exist in the list
                if (rm != null)
                {
                    //get first child, since it's a button object first child is the text object, and set the display text to the room name
                    rm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
                    //add on click event to run photon join room function
                    rm.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        m_PunLogger.AddLogMsg($"Joining room '{room.Name}'");
                        PhotonNetwork.JoinRoom(room.Name);
                    });
                }
                else
                {
                    m_PunLogger.AddLogMsg($"Duplicate room({room.Name}) detected");
                }
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        m_PunLogger.AddLogMsg($"New player ({newPlayer.NickName}) entered the room");
        //add player's name to the display list
        m_PlayerListing.AddPlayer(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        m_PunLogger.AddLogMsg($"Player ({otherPlayer.NickName}) left the room");
        //remove player's name from list
        m_PlayerListing.RemovePlayer(otherPlayer);
        //checks if original room owner has left, if so leaves room
        if (PhotonNetwork.CurrentRoom.masterClientId != 1)
        {
            PhotonNetwork.LeaveRoom();
        }

    }
    #endregion

    public void CreateJoinRoom(TMP_InputField _ipF)
    {
        m_PunLogger.AddLogMsg($"Creating and joining room '{_ipF.textComponent.text}'");
        RoomOptions RmOp= new RoomOptions();
        RmOp.MaxPlayers = 5;
        PhotonNetwork.CreateRoom(_ipF.textComponent.text, RmOp);
        //StartCoroutine(m_RoomListing.ClearAllChildObjects());
    }
    public void LeaveRoom()
    {
        m_PunLogger.AddLogMsg($"Leaving room '{PhotonNetwork.CurrentRoom.Name}'");
        /*if (m_AmRoomOwner)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.EmptyRoomTtl = 0;
            PhotonNetwork.CurrentRoom.PlayerTtl= 0;
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)
                {
                    Debug.Log(PhotonNetwork.CurrentRoom.Players[i].NickName);
                }
            }
        }*/
        /*Debug.Log($"MasterClientID: {PhotonNetwork.CurrentRoom.MasterClientId}");*/
        PhotonNetwork.LeaveRoom();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
