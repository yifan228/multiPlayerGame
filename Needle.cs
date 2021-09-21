using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Needle : MonoBehaviour
{
    private bool injured;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" &&TeamManager.instance.team==0)
        {
            GameObject player = collision.gameObject;
            injured = true;
            StartCoroutine(Injuring(player));
        }else if(collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine && TeamManager.instance.team != 0)
        {
            collision.GetComponent<PhotonView>().RPC("Death", RpcTarget.AllBuffered);
            collision.GetComponent<mainchar>().DisableInputs = true;
            GameManager.instance.enableRespawn();
        }
    }

    IEnumerator Injuring(GameObject player)
    {
        while (injured)
        {
            player.GetComponent<health>().Hp -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
            injured = false;
        }
    }
}
