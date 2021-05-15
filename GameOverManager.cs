using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOverManager : MonoBehaviourPun
{
    public GameObject youWinner;
    public GameObject notWinner;
    private void Start()
    {
        youWinner.SetActive(false);
        notWinner.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (GameOver.instance.AmIWinner == true)
        {
            youWinner.SetActive(true);
            notWinner.SetActive(false);
        }
        else {
            notWinner.SetActive(true);
            youWinner.SetActive(false);
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
