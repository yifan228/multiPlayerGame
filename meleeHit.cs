using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//直接施力
public class meleeHit : MonoBehaviourPun
{
    public SpriteRenderer mainchaRSprite;

    public SpriteRenderer RHitRange;
    bool IsPlayer;
    private Rigidbody2D otherPlayer;

    [SerializeField] private GameObject myplayerWeapon;

    private float hitCost=0.9f;
    private float power = 5f;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IsPlayer = true;
            otherPlayer = collision.GetComponent<Rigidbody2D>();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag =="Player")
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

    
    private Vector2 CalculateVec2()
    {
        Vector2 vector = otherPlayer.transform.position - (this.transform.position - new Vector3(0.3f, 0f, 0f));
        Vector2 force = vector.normalized * power;
        return force;
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
            photonView.RPC("SpellCostHit", RpcTarget.AllBuffered, hitCost);
           
            if (IsPlayer == true)
            {
                Vector2 vector2 = CalculateVec2();
                
                otherPlayer.GetComponent<mainchar>().photonView.RPC("BeHitten", RpcTarget.AllBuffered,vector2);
            }
            else { return; }
            
        }
    }
}


