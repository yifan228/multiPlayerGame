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

    private void Awake()
    {
        if (photonView.IsMine)
        {
            instance = this;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.GetComponent<mainchar>().IsDef);
        if (collision.tag == "Player" && collision.GetComponent<mainchar>().IsDef==false)
        {
           
            Debug.Log(collision.GetComponent<mainchar>().playerMasterName);
            Debug.Log("Hahahaha");


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
            Debug.Log("?");
        }
    }//不需要用photonveiw因為其他人的畫面上也有其他的玩家大便，玩家大便的isdef已經同步
     

    [PunRPC]
    public void GameoverScene()
    {
        PhotonNetwork.LoadLevel("GameOver");
    }//declare gameover to everyone

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(Master);
    //    }
    //    else if(stream.IsReading)
    //    {
    //        Master = (string)stream.ReceiveNext();
    //    }
    //}

    //[PunRPC]
    //public void SetMasterWinnerName(string text)
    //{
    //    Master = text;

    //}先不用角色名字


}
