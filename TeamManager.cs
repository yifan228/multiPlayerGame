using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TeamManager : MonoBehaviourPunCallbacks
{
    public static TeamManager instance;
    public GameObject localPlayer;
    public GameObject LSP;//left team spawn point
    public GameObject RLP;
    // Start is called before the first frame update
    [SerializeField]GameObject LSpBtn;
    [SerializeField] GameObject RSpBtn;
    [SerializeField] GameObject ReJBtn;
    [SerializeField] GameObject StartBtn;
    [SerializeField] GameObject OPeningcanvas;//upperthings

    public int team;
    public TypedLobby startGameLobby = new TypedLobby("StartGameLobby", LobbyType.Default);

    void Awake()
    {
        instance = this;
        team = 0;
        if (PhotonNetwork.IsMasterClient)
        {
            StartBtn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //redTeam
    public void OnClick_LBspawn()
    {
        team = -1;
        PhotonNetwork.Instantiate("PlayerBattleBlue",LSP.transform.position,Quaternion.identity);
        NotDes.instance.myTeam = -1;
        LSpBtn.SetActive(false);
        RSpBtn.SetActive(false);
        //set property in char
    }

    //blueTeam
    public void OnClick_RRspawn()
    {
        team = 1;
        PhotonNetwork.Instantiate("PlayerBattleRed",RLP.transform.position,Quaternion.identity);
        NotDes.instance.myTeam = 1;
        LSpBtn.SetActive(false);
        RSpBtn.SetActive(false);
    }


    public void OnClick_rechoose()
    {
        localPlayer.GetPhotonView().RPC("Des", RpcTarget.AllBuffered);
        LSpBtn.SetActive(true);
        RSpBtn.SetActive(true);
    }

    public void OnCLick_StartGame()
    {
        photonView.RPC("StartGame", RpcTarget.AllBuffered);
        photonView.RPC("SetCanF", RpcTarget.AllBuffered);
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
    }

    //set openingCanvas False
    [PunRPC]
    public void SetCanF()
    {
        OPeningcanvas.SetActive(false);
    }

    [PunRPC]
    public void StartGame()
    {    
        if (team == -1)
        {
            this.localPlayer.transform.position = localPlayer.transform.position + new Vector3(-50f, 100f, 0);
        }else if( team == 1)
        {
            this.localPlayer.transform.position = localPlayer.transform.position + new Vector3(50f, 100f, 0);
        }
        Debug.Log(localPlayer.name);

    }

    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //}
}
