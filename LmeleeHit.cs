using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LmeleeHit : MonoBehaviourPun
{
    public SpriteRenderer mainchaRSprite;

    public SpriteRenderer LHitRange;
    [SerializeField]bool IsPlayer;
    private Rigidbody2D otherPlayer;

    [SerializeField] private GameObject myplayerWeapon;

    private float hitCost=0.9f;
    private float power = 5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "Player" )
        {
            IsPlayer = true;
            otherPlayer = collision.GetComponent<Rigidbody2D>();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsPlayer = false;
            otherPlayer = null;
        }
    }

    [PunRPC]
    public void TurnOnSprite()
    {
        LHitRange.enabled = true;
        StartCoroutine(TurnOff());
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(0.5f);
        LHitRange.enabled = false;
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

    [PunRPC]
    private void SpellCostHit(float cost)
    {
            myplayerWeapon.GetComponent<Throw>().spellAmount.fillAmount -= cost;
        
    }

    
    private Vector2 CalculateVec2()
    {
        Vector2 vector = otherPlayer.transform.position - (this.transform.position + new Vector3(0.3f, 0f, 0f));
        Vector2 force = vector.normalized * power;
        return force;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && photonView.IsMine && myplayerWeapon.GetComponent<Throw>().spellAmount.fillAmount > hitCost)
        {
            
            photonView.RPC("TurnOnSprite", RpcTarget.AllBuffered);
            photonView.RPC("MainCharSpriteXTrue", RpcTarget.AllBuffered);
            photonView.RPC("SpellCostHit", RpcTarget.AllBuffered, hitCost);

            if (IsPlayer == true)
            {
                Vector2 vector2 = CalculateVec2();

                otherPlayer.GetComponent<PhotonView>().RPC("BeHitten", RpcTarget.AllBuffered, vector2);
            }
            else { return; }
        }
    }
}