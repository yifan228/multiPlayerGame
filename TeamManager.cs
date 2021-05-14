using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamManager : MonoBehaviourPun
{
    public static TeamManager instance;
    public GameObject localPlayer;
    public GameObject LSP;//left team spawn point
    public GameObject RLP;
    // Start is called before the first frame update
    [SerializeField]GameObject LSpBtn;
    [SerializeField] GameObject RSpBtn;
    [SerializeField] GameObject ReJBtn;

    public int team;
    void Awake()
    {
        instance = this;
        team = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //redTeam
    public void OnClick_LBspawn()
    {
        PhotonNetwork.Instantiate("PlayerBattleBlue",LSP.transform.position,Quaternion.identity);
        team = -1;
        LSpBtn.SetActive(false);
        RSpBtn.SetActive(false);
        //set property in char
    }

    //blueTeam
    public void OnClick_RRspawn()
    {
        PhotonNetwork.Instantiate("PlayerBattleRed",RLP.transform.position,Quaternion.identity);
        team = 1;
        LSpBtn.SetActive(false);
        RSpBtn.SetActive(false);
    }


    public void OnClick_rechoose()
    {
        localPlayer.GetPhotonView().RPC("Des", RpcTarget.AllBuffered);
        LSpBtn.SetActive(true);
        RSpBtn.SetActive(true);
    }
    
}
