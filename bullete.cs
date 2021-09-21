using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using UnityEngine.Experimental.Rendering.Universal;

public class bullete : MonoBehaviourPun
{
    public float destroyTime =5f;
    public float damage =0.6f;
    public GameObject BulleteLit;
    [SerializeField]ParticleSystem parsys;
    [SerializeField]GameObject ParSystem;
    [SerializeField]SpriteRenderer SpriteRend;
    [SerializeField] Rigidbody2D rig;
    [SerializeField] Collider2D coll2D;

    IEnumerator destroyBullete()
    {
        yield return new WaitForSeconds(destroyTime);
        photonView.RPC("Destroy", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void Destroy()
    {
        SpriteRend.enabled = false;
        rig.isKinematic = true;
        coll2D.enabled = false;
        ParSystem.SetActive(true);
        parsys.Play();
        Destroy(this.gameObject,0.5f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.IsMine)
        {
            return;
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
                return;
            }
        }
        
    }
    protected virtual void Coloring()
    {
        if (!photonView.IsMine)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0);
        }
    }

    private void Start()
    {
        if (GameManager.instance.localPlayer.GetComponent<mainchar>().LitOn == -1)
        {
            BulleteLit.SetActive(true);
        }
        else
        {
            BulleteLit.SetActive(false);
        }

        if (GameManager.instance.localPlayer.GetComponent<mainchar>().InSpace)
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
       StartCoroutine(destroyBullete());
    }
    private void Update()
    {
        Coloring();
    }


}
