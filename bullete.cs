using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bullete : MonoBehaviourPun
{
    public float destroyTime =5f;
    public float damage = 0.15f;

    
    
    IEnumerator destroyBullete()
    {
        yield return new WaitForSeconds(destroyTime);
        Debug.Log("timeToDes");
        photonView.RPC("Destroy", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void Destroy()
    {
        Destroy(this.gameObject);
   
        Debug.Log("dest");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        else 
        {
            Debug.Log("haha");
        }

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null)
        {
            if (target.tag == "Player" && (!target.IsMine))
            {
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, damage);
                photonView.RPC("Destroy", RpcTarget.AllBuffered);
            }
            else
            {
                Debug.Log("notmine");
                return;
            }
        }
        
    }
    public void coloring()
    {
        if (!photonView.IsMine)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(256, 0, 0);
        }
    }

    private void Start()
    {
       StartCoroutine(destroyBullete());
    }
    private void Update()
    {
        coloring();
    }
}
