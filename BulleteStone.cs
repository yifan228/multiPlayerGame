using System;
using UnityEngine;
using Photon.Pun;
public class BulleteStone:MonoBehaviourPun
{
    public float damage = 0.5f;
    bool HaveAttacted = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;
        if(obj.tag == "Player")
        {
            if (obj.GetComponent<mainchar>().Stat == "scissor" && obj.GetPhotonView().IsMine && !HaveAttacted)
            {
                obj.GetPhotonView().RPC("HealthUpdate", RpcTarget.AllBuffered, damage);
                HaveAttacted = true;
                photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }else if(obj.GetComponent<mainchar>().Stat == "paper" && obj.GetPhotonView().IsMine)
            {
                photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }
        }
        else if(obj.tag == "Block")
        {
            if (obj.GetComponent<CreatedBlock>().Stat == "scissor")
            {
                obj.GetComponent<PhotonView>().RPC("Des", RpcTarget.AllBuffered);
                //photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }
            else if (obj.GetComponent<CreatedBlock>().Stat == "paper")
            {
                photonView.RPC("Ddestroy", RpcTarget.AllBuffered);
            }
        }
        //else if(obj.tag == "Bullete")
        //{
        //    if (obj.name == "scissor(Clone)")
        //        Destroy(obj);
        //}

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullete")
        {
            if (collision.gameObject.name == "scissor(Clone)")
                Destroy(collision.gameObject);//不知道這樣會不會出事
        }
    }

    [PunRPC]
    void Ddestroy()
    {
        Destroy(gameObject);
    }
}
