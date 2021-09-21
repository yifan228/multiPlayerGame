using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulleteScissor : MonoBehaviourPun
{
    public float damage = 0.5f;
    bool HaveAttacted = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            if (obj.GetComponent<mainchar>().Stat == "paper" && obj.GetPhotonView().IsMine && !HaveAttacted)
            {
                obj.GetPhotonView().RPC("HealthUpdate", RpcTarget.AllBuffered, damage);
                HaveAttacted = true;
                photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }else if (obj.GetComponent<mainchar>().Stat == "stone" && obj.GetPhotonView().IsMine)
            {
                photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }
        }
        else if(obj.tag == "Block")
        {
            if (obj.GetComponent<CreatedBlock>().Stat == "paper"&& obj.GetComponent<PhotonView>().IsMine)
            {
                obj.GetComponent<PhotonView>().RPC("Des", RpcTarget.AllBuffered);
                //photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }
            else if(obj.GetComponent<CreatedBlock>().Stat == "stone"&& obj.GetComponent<PhotonView>().IsMine)
            {
                photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }
        }
        //else if(obj.tag == "Bullete")
        //{
        //    if (obj.name == "paper(Clone)")
        //        Destroy(obj);
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullete")
        {
            if (collision.gameObject.name == "paper(Clone)")
                Destroy(collision.gameObject);
        }
    }

    [PunRPC]
    void Ddestroy()
    {
        Destroy(gameObject);
    }
}
