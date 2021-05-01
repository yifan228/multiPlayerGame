using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class meleeHit : MonoBehaviourPun
{
    public SpriteRenderer mainchaRSprite;

    public SpriteRenderer RHitRange;
    bool IsPlayer;
    private Rigidbody2D otherPlayer;

    [SerializeField] private GameObject myplayerWeapon;

    private float hitCost=0.9f;
    private float power = 150f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine == false)
        {
            IsPlayer = true;
            otherPlayer = collision.GetComponent<Rigidbody2D>();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag =="Player" && collision.GetComponent<PhotonView>().IsMine == false)
        {
            IsPlayer = false;
            otherPlayer = null;
        }
    }

    [PunRPC]
    public void TurnOnSprite()
    {
        RHitRange.enabled = true;
        StartCoroutine(TurnOff());
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(0.5f);
        RHitRange.enabled = false;
    }

    
    private void CalculateForceAndHIt()
    {
        Vector2 vector = otherPlayer.transform.position - (this.transform.position - new Vector3(0.3f,0f,0f));
        Vector2 force = vector.normalized * power;
        otherPlayer.GetComponent<PhotonView>().RPC("AdForce", RpcTarget.AllBuffered, force);
    }

    [PunRPC]
    private void SpellCostHit(float cost)
    {
            myplayerWeapon.GetComponent<Throw>().spellAmount.fillAmount -= cost;
        
    }

    [PunRPC]
    private void MainCharSpriteXFalse()
    {
        mainchaRSprite.flipX = false;
    }

    [PunRPC]
    private void MainCharSpriteXTrue()
    {
        mainchaRSprite.flipX = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && photonView.IsMine && myplayerWeapon.GetComponent<Throw>().spellAmount.fillAmount > hitCost)
        {
            //Debug.Log(this.transform.position);
            photonView.RPC("TurnOnSprite", RpcTarget.AllBuffered);
            photonView.RPC("MainCharSpriteXFalse", RpcTarget.AllBuffered);
            photonView.RPC("SpellCostHit", RpcTarget.AllBuffered,hitCost);
            if (IsPlayer == true)
            {
                //Debug.Log(otherPlayer.transform.position);
                //Debug.Log(otherPlayer.transform.position - (this.transform.position - new Vector3(0.3f, 0f, 0f)));
                CalculateForceAndHIt();
            }
            else { return; }
        }
    }
}


