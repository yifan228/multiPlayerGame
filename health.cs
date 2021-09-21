using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class health : MonoBehaviourPun  
{
    public bool Isalive=true;
    public Image fillImage;

    public float Hp = 1;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public BoxCollider2D coll;
    public ParticleSystem particle;

    public GameObject playercanvas;
    

    public void checkHp()
    {
        if (photonView.IsMine && Hp <= 0)
        {
            photonView.RPC("Death", RpcTarget.AllBuffered);
            GameManager.instance.localPlayer.GetComponent<mainchar>().DisableInputs = true;
            GameManager.instance.enableRespawn();
            
        }
    }
    
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
    
    [PunRPC]
    public void Death()
    {
        rb.isKinematic = true;
        sr.enabled = false;
        coll.enabled = false;
        Isalive = false;
        particle.Play();
        //can't throw
        //playercanvas.SetActive(false); //死掉時還可以說話
       
    }

    [PunRPC]
    public void Revive()
    {
        rb.isKinematic = false;
        sr.enabled = true;
        coll.enabled = true ;
        //playercanvas.SetActive(true);
        GameManager.instance.respawnUi.SetActive(false);
        GameManager.instance.startRespawn = false;
        fillImage.fillAmount = 1;
        Hp = 1f;
        Isalive = true;
    }

   [PunRPC]
   public void HealthUpdate(float damage)//子彈攻擊到的時候
    {
        if (Hp >= 0)
        {
            Hp -= damage;
            //checkHp();
        }
    }
    private void Update()
    {
        fillImage.fillAmount = Hp;

        if (Isalive)
        {
            checkHp();
        }
        
        if (Hp < 1f && Hp > 0)
        {
            Hp += 0.1f * Time.deltaTime;
            if (Hp > 1f)
            {
                Hp = 1f;
            }
        }

    }

    
}
