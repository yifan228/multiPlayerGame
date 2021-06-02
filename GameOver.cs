using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOver : MonoBehaviourPun
{
    public string Master;//set name to gameOverManager 要用string 不要腦才用text，text要拖一個unity的物件進去，才會有參考
    //public string winnerName;

    public static GameOver instance;
    public bool AmIWinner;
    public int WinTeam;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            instance = this;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TeamManager.instance.team == 0)
        {
            //Debug.Log(collision.GetComponent<mainchar>().IsDef);
            if (collision.tag == "Player" && collision.GetComponent<mainchar>().IsDef == false)
            {

                //Debug.Log(collision.GetComponent<mainchar>().playerMasterName);
                //Debug.Log("Hahahaha");


                //string Name = collision.GetComponent<mainchar>().playerMasterName;
                //string a = collision.GetComponent<mainchar>().playerName.text;

                //photonView.RPC("SetMasterWinnerName", RpcTarget.AllBuffered,a);

                //Master = collision.GetComponent<mainchar>().playerName.text;

                if (collision.GetComponent<PhotonView>().IsMine)
                {
                    AmIWinner = true;
                    photonView.RPC("GameoverScene", RpcTarget.AllBuffered);
                }
                else { AmIWinner = false; }

                DontDestroyOnLoad(this.gameObject);
                Destroy(this.gameObject, 1.5f);
            }
            else 
            {
                if(collision.tag =="Player"&&collision.GetComponent<mainchar>().teamNum == 1)
                {
                    WinTeam = 1;
                }
                else if(collision.tag == "Player" && collision.GetComponent<mainchar>().teamNum == -1)
                {
                    WinTeam = -1;
                }
            }
        }
    }
     

    [PunRPC]
    public void GameoverScene()
    {
        PhotonNetwork.LoadLevel("GameOver");
    }


}
