using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class batSystem : MonoBehaviourPun
{
    //public CircleCollider2D batRange;
    public Camera charcamera;

    public Vector2 HitForceVector;
    public Vector2 HitPlayerForceVector;

    [Header("determinationBool")]
    public bool canHit =false;
    public bool canHitPlayer = false;
    public bool IsBullete;
    public bool IsmyExchaneBullete;
    public bool Isenemy;

    public float BatForce = 500;
    private Rigidbody2D HitGoal;
    private Rigidbody2D HitGaolPlayer;
    private Vector3 GaolPosition;
    private Vector2 GaolVelocity;
    
    
    public static batSystem instance;

    private void Awake()
    {
        instance = this;
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.tag == "Bullete")
    //    {
    //        HitGoal = col.GetComponent<Rigidbody2D>();
    //        canHit = true;          
    //    }

    //    if(col.tag == "Player")
    //    {
    //        HitGaolPlayer = col.GetComponent<Rigidbody2D>();
    //        canHitPlayer = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D col)
    //{
    //    if (col.tag == "Bullete")
    //    {
    //        canHit = false;
    //    }

    //    if(col.tag == "Player")
    //    {
    //        canHitPlayer = false;
    //    }
    //}

    void HitOrChangePos()
    {
        if (IsBullete && canHit)
        {
            

                Vector2 mousePosition = charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                Vector2 batPosition = this.transform.position;
                HitForceVector = (mousePosition - batPosition) * BatForce;
                HitGoal.AddForceAtPosition(HitForceVector, HitGoal.transform.position, ForceMode2D.Impulse);
                //Debug.Log("hitting");
                //Debug.Log(HitForceVector);

        }else if(IsBullete && (!canHit) && (IsmyExchaneBullete))
        {
            
            mainchar.instance.setPosition(GaolPosition);
            mainchar.instance.setVelocity(GaolVelocity);
        }else if (canHitPlayer && Isenemy && (!IsBullete))
        {
            Vector2 mousePosition = charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Vector2 batPosition = this.transform.position;
            HitPlayerForceVector = (mousePosition - batPosition) * BatForce *10f;
            HitGaolPlayer.AddForceAtPosition(HitPlayerForceVector, HitGaolPlayer.transform.position, ForceMode2D.Impulse);
        }

        canHit = false;
        canHitPlayer = false;
        IsBullete = false;
        Isenemy = false;
        IsmyExchaneBullete = false;
    }

    public bool DetectIsInsideCircle(float x,float y,float R,float xo,float yo)
    {
        if ((x - xo) * (x - xo) + (y - yo) * (y - yo) <= R)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.CircleCast(charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,10f)),0.3f,Vector2.zero);
                //Debug.Log(hit.transform.position);
                if (hit)
                {
                    if (hit.collider.tag == "Bullete")
                    {
                        //Debug.Log("cameraDetectCanHit");
                        IsBullete = true;
                        canHit = DetectIsInsideCircle(hit.transform.position.x, hit.transform.position.y, 2f, this.transform.position.x, this.transform.position.y);
                        if (canHit)
                        {
                            HitGoal = hit.collider.GetComponent<Rigidbody2D>();
                        }
                        else if (hit.collider.GetComponent<PhotonView>().IsMine && !canHit)
                        {
                            IsmyExchaneBullete = true;
                            
                            GaolPosition =(hit.collider.transform.position);
                            //Debug.Log(hit.collider.GetComponent<Rigidbody2D>().velocity);
                            GaolVelocity = hit.collider.GetComponent<Rigidbody2D>().velocity;
                            hit.collider.GetComponent<PhotonView>().RPC("Destroy",RpcTarget.AllBuffered);
                        }
                        else
                        {
                            IsmyExchaneBullete = false;
                        }
                    }
                    //else if (hit.collider.tag == "Player")
                    //{
                    //    Debug.Log("clickOnI");
                    //    IsBullete = false;
                    //    if (hit.collider.GetComponentInParent<PhotonView>().IsMine == true)
                    //    {
                    //        Debug.Log("itsMe");
                    //        Isenemy = true;
                    //        HitGaolPlayer = hit.collider.GetComponent<Rigidbody2D>();
                    //    }
                    //}


                }
                

                //Debug.Log(Input.mousePosition);
                //Debug.Log(charcamera.ScreenToWorldPoint(Input.mousePosition+ charcamera.WorldToScreenPoint(charcamera.transform.position)));
                //Debug.Log(charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)));
                //Debug.Log(this.transform.position);
                HitOrChangePos();
            }
        }
    }
}
