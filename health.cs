using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class health : MonoBehaviourPun,IPunObservable
{
    public Image fillImage;

    public float Hp = 1;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D coll;

    public GameObject playercanvas;
    

    public void checkHp()
    {
        if (photonView.IsMine && Hp <= 0)
        {
            photonView.RPC("Death", RpcTarget.AllBuffered);
            gameObject.GetComponent<mainchar>().DisableInputs = true;
            GameManager.instance.enableRespawn();

        }
    }
    #region test
    //void aa()
    //{
    //    if (Input.GetKey(KeyCode.G))
    //    {


    //        photonView.RPC("Death", RpcTarget.AllBuffered);
    //        gameObject.GetComponent<mainchar>().DisableInputs = true;
    //        GameManager.instance.enableRespawn();
    //    }

    //}

    //private void Update()
    //{
    //    aa();
    //}
    #endregion
    [PunRPC]
    public void Death()
    {
        rb.isKinematic = true;
        sr.enabled = false;
        coll.enabled = false;
        playercanvas.SetActive(false);
       
    }

    [PunRPC]
    public void Revive()
    {
        rb.isKinematic = false;
        sr.enabled = true;
        coll.enabled = true ;
        playercanvas.SetActive(true);
        GameManager.instance.respawnUi.SetActive(false);
        GameManager.instance.startRespawn = false;
        fillImage.fillAmount = 1;
        Hp = 1f;
        
    }

   [PunRPC]
   public void HealthUpdate(float damage)
    {
        fillImage.fillAmount -= damage;

        Hp = fillImage.fillAmount;
        checkHp();
    }
    private void Update()
    {
        if (Hp < 1f && Hp > 0)
        {
            Hp += 0.1f * Time.deltaTime;
            if (Hp > 1f)
            {
                Hp = 1f;
            }
        }

        fillImage.fillAmount = Hp; 
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Hp);

        }else if (stream.IsReading)
        {
            this.Hp = (float)stream.ReceiveNext();
        }
    }
}
