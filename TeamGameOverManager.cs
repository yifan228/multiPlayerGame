using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamGameOverManager : MonoBehaviour
{
    public GameObject LBTeam;
    public GameObject RRteam;
    // Start is called before the first frame update
    void Start()
    {
        LBTeam.SetActive(false);
        RRteam.SetActive(false);
        if (NotDes.instance.WinTeam == 1)
        {
            RRteam.SetActive(true);
        }
        else if (NotDes.instance.WinTeam == -1)
        {
            LBTeam.SetActive(true);
        }
    }

    

    public void LeaveBtn()
    {
        Destroy(NotDes.instance.gameObject);
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
