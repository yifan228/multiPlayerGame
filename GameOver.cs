using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    private PhotonView view;
    
    //public static GameOver instance ;

    //private void Awake()
    //{
    //    instance = this;
    //}

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine && (GameManager.instance.localPlayer.GetComponent<mainchar>().IsDef ==false))
        {
            view = collision.GetComponent<PhotonView>();
            Debug.Log(collision.GetComponentInChildren<mainchar>().IsDef);
            Debug.Log("Hahahaha");
            view.RPC("GameoverScene", RpcTarget.AllBuffered);
        }
        else
        {
            //Debug.Log("?");
        }
    }//need to know IsDef attribute to judge if the player is Defender  這個地方很奇怪，如果GameOverScene的函示裡沒有加上photonV.ismine 會先跑出?????然後執行gameover的方法
    //不知道是不是因為collision的判斷，取樣到另外的玩家  

}
