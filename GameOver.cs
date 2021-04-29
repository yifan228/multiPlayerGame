using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameOver : MonoBehaviourPun
{
    public bool IsDef=false;//搞人的玩家
    public static GameOver instance ;

    private void Awake()
    {
        instance = this;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !IsDef)
        {
            Debug.Log(IsDef);
            Debug.Log("gameOver");
            collision.GetComponent<PhotonView>().RPC("GameoverScene", RpcTarget.AllBuffered);
        }
    }

}
