using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class menuManager : MonoBehaviourPunCallbacks
{
    public static menuManager instance;

    [SerializeField]private GameObject nameSpace, roomSpace;

    [SerializeField]private GameObject CRBtn, JRBtn, NBtn;
    
    [SerializeField] private InputField createRoomIF,joinRoomIF,nameIF;


    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
   

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to master!!");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("connected to lobby");
        nameSpace.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    #region UImethod
    public void Onclick_CreateRoomBtn()
    {

        PhotonNetwork.CreateRoom(createRoomIF.text,new RoomOptions { MaxPlayers = 4},null);
    }

    public void Onclick_JoinRoom()
    {
        RoomOptions RO = new RoomOptions();
        RO.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(joinRoomIF.text, RO, TypedLobby.Default);
    }

    public void Name()
    {
        PhotonNetwork.NickName = nameIF.text;
        nameSpace.SetActive(false);
        roomSpace.SetActive(true);
    }
    public void onNameFieldChange()
    {
        if (nameIF.text.Length >= 2)
        {
            NBtn.SetActive(true);
        }
    }
    #endregion
}
