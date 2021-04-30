using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LmeleeHit : MonoBehaviourPun
{
    public SpriteRenderer mainchaRSprite;

    public SpriteRenderer LHitRange;
    [SerializeField]bool IsPlayer=false;
    private Rigidbody2D otherPlayer;

    [SerializeField] private GameObject myplayerWeapon;

    private float hitCost=0.9f;
    [HideInInspector]public float power = 100f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == "Player" && !collision.GetComponent<PhotonView>().IsMine)
        {
            IsPlayer = true;
            otherPlayer = collision.GetComponent<Rigidbody2D>();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine == false)
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

    private void CalculateForceAndHIt()
    {
        Vector2 vector = otherPlayer.transform.position - (this.transform.position - new Vector3(0.3f, 0f, 0f));
        Vector2 force = vector.normalized * power;
        otherPlayer.GetComponent<PhotonView>().RPC("AdForce", RpcTarget.AllBuffered, force);
        Debug.Log("itsAddedforce");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && photonView.IsMine && myplayerWeapon.GetComponent<Throw>().spellAmount.fillAmount > hitCost)
        {
            Debug.Log(this.transform.position);
            photonView.RPC("TurnOnSprite", RpcTarget.AllBuffered);
            photonView.RPC("MainCharSpriteXTrue", RpcTarget.AllBuffered);
            photonView.RPC("SpellCostHit", RpcTarget.AllBuffered, hitCost);
            if (IsPlayer == true)
            {
                CalculateForceAndHIt();
            }
            else { return; }
        }
    }
}