using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathTrigger : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PhotonView>().RPC("Death", RpcTarget.AllBuffered);
            GameManager.instance.enableRespawn();
        }
    }
}
