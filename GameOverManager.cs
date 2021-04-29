using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOverManager : MonoBehaviourPun
{
    public Text Winner;

    private void Start()
    {
        Winner.text = "efan";
    }

    public void LeaveBtn()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
        
    }
}
