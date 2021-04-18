using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class bullete : MonoBehaviourPun
{
    public float destroyTime =3f;
    public float damage = 0.3f;
    
    
    IEnumerator destroyBullete()
    {
        yield return new WaitForSeconds(destroyTime);
        this.GetComponent<PhotonView>().RPC("Destroy", RpcTarget.AllBuffered);
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

        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if(target!=null&&(!target.IsMine||target.IsSceneView))
        {
            if (target.tag =="Player")
            {
                target.RPC("HealthUpdate", RpcTarget.AllBuffered,damage);
            }

            photonView.RPC("Destroy", RpcTarget.AllBuffered);
        }
    }
    public void coloring()
    {
        if (!photonView.IsMine)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(256, 0, 0);
        }
    }
    private void Update()
    {
        coloring();
    }
}
