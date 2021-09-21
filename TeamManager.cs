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
    [SerializeField] Transform LeftSpawnPoint;
    [SerializeField] Transform RiteSpawnPoint;
    [Header("sceneCam")] public GameObject SceneCam;
    public int team;
    public TypedLobby startGameLobby = new TypedLobby("StartGameLobby", LobbyType.Default);
    //public GameObject Blue, Red;
    [Header("OnPlaying")]
    private bool onPlaying=false;
    public GameObject LandPortalR,LandPortalL;

    private void Awake()//要在gamemanager實例生成之前gamemanager才能參考
    {
        instance = this;
        //team = 0;
    }

    void Start()
    {
        //SpawnMyPlayer();
        
        if (PhotonNetwork.IsMasterClient &&StartBtn!=null)
        {
            StartBtn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (team!=0&& onPlaying)
        {
            if (localPlayer == null)
            {
                LandPortalL.SetActive(true);
                LandPortalR.SetActive(true);
            }
        }
    }

    
    public void OnClick_LBspawn()
    {
        team = -1;
        SceneCam.SetActive(false);
        localPlayer = PhotonNetwork.Instantiate("PlayerBattleBlue",LSP.transform.position,Quaternion.identity);
        //Blue.GetComponent<mainchar>().photonView.RPC("Appear", RpcTarget.AllBuffered);
        NotDes.instance.myTeam = -1;
        LSpBtn.SetActive(false);
        RSpBtn.SetActive(false);
        //set property in char
    }

    
    public void OnClick_RRspawn()
    {
        team = 1;
        SceneCam.SetActive(false);
        localPlayer= PhotonNetwork.Instantiate("PlayerBattleRed",RLP.transform.position,Quaternion.identity);
        //Red.GetComponent<mainchar>().photonView.RPC("Appear", RpcTarget.AllBuffered);
        NotDes.instance.myTeam = 1;
        LSpBtn.SetActive(false);
        RSpBtn.SetActive(false);
    }

    public void OnClick_RedCreate()
    {
        team = 1;
        SceneCam.SetActive(false);
        localPlayer = PhotonNetwork.Instantiate("PlayerBattleRed", RiteSpawnPoint.position, Quaternion.identity);
        //Red.GetComponent<mainchar>().photonView.RPC("Appear", RpcTarget.AllBuffered);
        NotDes.instance.myTeam = 1;
        LandPortalL.SetActive(false);
        LandPortalR.SetActive(false);
    }

    public void OnClick_BCreate()
    {
        team = -1;
        SceneCam.SetActive(false);
        localPlayer = PhotonNetwork.Instantiate("PlayerBattleBlue", LeftSpawnPoint.position, Quaternion.identity);
        //Blue.GetComponent<mainchar>().photonView.RPC("Appear", RpcTarget.AllBuffered);
        NotDes.instance.myTeam = -1;
        LandPortalR.SetActive(false);
        LandPortalL.SetActive(false);
    }

    //public void SpawnMyPlayer()
    //{
    //    Red = PhotonNetwork.Instantiate("PlayerBattleRed", RLP.transform.position, Quaternion.identity);
    //    Blue = PhotonNetwork.Instantiate("PlayerBattleBlue", LSP.transform.position, Quaternion.identity);
    //}
    public void OnClick_rechoose()
    {
        //localPlayer.GetComponent<mainchar>().photonView.RPC("Des",RpcTarget.AllBuffered);
        PhotonNetwork.Destroy(localPlayer);
        localPlayer = null;
        Invoke("RechooseSetting", 0.5f);
    }//這樣做相機才不會有視覺殘留，數值零界點可能要超過零點二
    public void RechooseSetting()
    {
        SceneCam.SetActive(true);
        LSpBtn.SetActive(true);
        RSpBtn.SetActive(true);
    }
    public void OnCLick_StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        photonView.RPC("StartGame", RpcTarget.AllBuffered);
        photonView.RPC("SetCanF", RpcTarget.AllBuffered);
    }

    //set openingCanvas False
    [PunRPC]
    public void SetCanF()
    {
        OPeningcanvas.SetActive(false);
        onPlaying = true;
        
    }

    [PunRPC]
    public void StartGame()
    {    
        if (team == -1)
        {
            localPlayer.transform.position = LeftSpawnPoint.position;
        }else if(team == 1)
        {
            localPlayer.transform.position = RiteSpawnPoint.position;
        }
        //Debug.Log(localPlayer.name);

    }

    //IEnumerator Wait()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //}
}
