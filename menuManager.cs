using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Chat;
using Photon.Realtime;

public class menuManager : MonoBehaviourPunCallbacks
{
    public static menuManager instance;

    [SerializeField] private GameObject nameSpace, roomSpace, playerListScreen,BtListScreen;

    [SerializeField] private GameObject CRBtn, JRBtn, NBtn;

    [SerializeField] private InputField createRoomIF, joinRoomIF, nameIF,talkbox;

    [SerializeField] private Text playerListText;

    [SerializeField] private Transform talkListTran;

    

    public GameObject TalkBoxPref;

    public bool IsjoinBT;
    private TypedLobby BattleLobby = new TypedLobby("BTLobby",LobbyType.Default);
    private TypedLobby TeamLobby = new TypedLobby("TMLobby",LobbyType.Default);

    

    public Button startGameBtn;
    //public string Myname;

    private void Awake()
    {
        

        PhotonNetwork.ConnectUsingSettings();
        instance = this;
        
    }
   

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master!!");
        nameSpace.SetActive(true);
        //PhotonNetwork.JoinLobby();
    }
    //public override void OnJoinedLobby()
    //{
    //    Debug.Log("connected to lobby");
    //    nameSpace.SetActive(true);

    //}

    public override void OnJoinedRoom()
    {
        if (IsjoinBT)
        {
            roomSpace.SetActive(false);
            playerListScreen.SetActive(true);
            photonView.RPC("UpdateLobbyUI", RpcTarget.All);
        }
        else
        {
            roomSpace.SetActive(false);
            PhotonNetwork.LoadLevel(3);

        }
    }

    
    public void Onclick_CreateRoomBtn()
    {
        NotDes.instance.RoomName = createRoomIF.text;
        PhotonNetwork.CreateRoom(createRoomIF.text);
    }

    public void Onclick_JoinRoom()
    {
        //RoomOptions RO = new RoomOptions();
        //RO.MaxPlayers = 4;
        NotDes.instance.RoomName = joinRoomIF.text;
        PhotonNetwork.JoinRoom(joinRoomIF.text);
    }


    //join battle mode
    public void Name()
    {
        PhotonNetwork.NickName = nameIF.text;
        NotDes.instance.MyName = nameIF.text;
        nameSpace.SetActive(false);
        roomSpace.SetActive(true);
        PhotonNetwork.JoinLobby(BattleLobby);
        IsjoinBT = true;
        //Myname = nameIF.text;
    }
    public void onNameFieldChange()
    {
        if (nameIF.text.Length >= 2)
        {
            NBtn.SetActive(true);
        }
    }

    public void onclickStartGameBtn()
    {
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        photonView.RPC("StartGame", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void onclickLeaveBtn()
    {
        PhotonNetwork.LeaveRoom();
        playerListScreen.SetActive(false);
        //PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    [PunRPC]
    public void UpdateLobbyUI()
    {
        playerListText.text = "";

        //display all the player currently in the lobby(room?) !!!need to test!!!
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }

        if (PhotonNetwork.IsMasterClient)
        {
            startGameBtn.interactable = true;
        }
        else
        {
            startGameBtn.interactable = false;
        }
    }

    //to join team mode
    public void ToJoinTeamLobby()
    {
        PhotonNetwork.NickName = nameIF.text;
        NotDes.instance.MyName = nameIF.text;
        nameSpace.SetActive(false);
        roomSpace.SetActive(true);
        PhotonNetwork.JoinLobby(TeamLobby);
        IsjoinBT = false;
    }

    //talk
    public void sendTalk()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            string text = talkbox.text+ "   by" + PhotonNetwork.NickName;
            photonView.RPC("SetTalkBoxPosition", RpcTarget.AllBuffered,text);
        }
    }
    [PunRPC] public void SetTalkBoxPosition(string text)
    {
        GameObject obj;//talkBox gameobject
        obj =GameObject.Instantiate(TalkBoxPref, new Vector3(0, 0, 0), Quaternion.identity);
        obj.transform.SetParent(talkListTran);

        obj.GetComponentInChildren<Text>().text = text ;

        talkbox.text = "";
    }
    private void Update()
    {
        sendTalk();
    }

}
