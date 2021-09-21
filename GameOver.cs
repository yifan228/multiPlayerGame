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
    public bool AmIWinner=false;
    public int WinTeam;

    public int num;
    private void Start()
    {
            instance = this;   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") //&& collision.GetComponent<mainchar>().IsDef == false)
        {
            if (TeamManager.instance.team == 0)
            {
            //Debug.Log(collision.GetComponent<mainchar>().IsDef);

                //Debug.Log(collision.GetComponent<mainchar>().playerMasterName);
                //Debug.Log("Hahahaha");


                //string Name = collision.GetComponent<mainchar>().playerMasterName;
                //string a = collision.GetComponent<mainchar>().playerName.text;

                //photonView.RPC("SetMasterWinnerName", RpcTarget.AllBuffered,a);

                //Master = collision.GetComponent<mainchar>().playerName.text;

                if (collision.GetComponent<PhotonView>().IsMine)
                {
                    AmIWinner = true;

                    photonView.RPC("SetWinner", RpcTarget.AllBuffered,PhotonNetwork.NickName);
                    photonView.RPC("GameoverScene", RpcTarget.AllBuffered);

                    DontDestroyOnLoad(this.gameObject);
                    Destroy(this.gameObject, 1.5f);
                }
                

            }
            else 
            {
                if(collision.tag =="Player"&&collision.name == "PlayerBattleBlue(Clone)"&&num==1)
                {
                
                    photonView.RPC("GameoverSceneTeamB", RpcTarget.AllBuffered);
                    //要改！！！！！！！！！
                    DontDestroyOnLoad(this.gameObject);
                    Destroy(this.gameObject, 1.5f);
                }
                else if(collision.tag == "Player" && collision.name == "PlayerBattleRed(Clone)"&&num==-1)
                {
            
                    photonView.RPC("GameoverSceneTeamR", RpcTarget.AllBuffered);
                    
                    DontDestroyOnLoad(this.gameObject);
                    Destroy(this.gameObject, 1.5f);
                }
            }
        }
    }
     

    [PunRPC]
    public void GameoverScene()
    {
        PhotonNetwork.LoadLevel("GameOver");
    }
    [PunRPC]
    public void SetWinner(string name)
    {
        NotDes.instance.WInName =name;
    }

    [PunRPC]
    public void GameoverSceneTeamB()
    {
        NotDes.instance.WinTeam = -1;
        PhotonNetwork.LoadLevel("Team");
    }

    [PunRPC]
    public void GameoverSceneTeamR()
    {
        NotDes.instance.WinTeam = 1;
        PhotonNetwork.LoadLevel("Team");
    }
}
