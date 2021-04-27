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
    public bool canHit =false;
    public bool IsBullete;
    private bool IsmyExchaneBullete;

    public float BatForce = 500;
    private Rigidbody2D HitGoal;
    private Vector3 GaolPosition;
    private Vector2 GaolVelocity;
    
    
    public static batSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullete")
        {
            HitGoal = col.GetComponent<Rigidbody2D>();
            canHit = true;          
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Bullete")
        {
            canHit = false;
        }
    }

    void HitOrChangePos()
    {
        if (IsBullete && canHit)
        {
            

                Vector2 mousePosition = charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                Vector2 batPosition = this.transform.position;
                HitForceVector = (mousePosition - batPosition) * BatForce;
                HitGoal.AddForceAtPosition(HitForceVector, HitGoal.transform.position, ForceMode2D.Impulse);;
                //Debug.Log("hitting");
                //Debug.Log(HitForceVector);

        }else if(IsBullete && (!canHit) && (IsmyExchaneBullete))
        {
            
            mainchar.instance.setPosition(GaolPosition);
            mainchar.instance.setVelocity(GaolVelocity);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), Vector2.zero);

                if (hit)
                {
                    if (hit.collider.tag == "Bullete")
                    {
                        //Debug.Log("cameraDetectCanHit");
                        IsBullete = true;
                        if (hit.collider.GetComponent<PhotonView>().IsMine && !canHit)
                        {
                            IsmyExchaneBullete = true;
                            
                            GaolPosition =(hit.collider.transform.position);
                            //Debug.Log(hit.collider.GetComponent<Rigidbody2D>().velocity);
                            GaolVelocity = hit.collider.GetComponent<Rigidbody2D>().velocity;
                            hit.collider.gameObject.SetActive(false);
                        }
                        else
                        {
                            IsmyExchaneBullete = false;
                            
                        }
                    }
                    else
                    {
                        IsBullete = false;
                    }

                }

                //Debug.Log(hit.collider.name);
                //Debug.Log(Input.mousePosition);
                //Debug.Log(charcamera.ScreenToWorldPoint(Input.mousePosition+ charcamera.WorldToScreenPoint(charcamera.transform.position)));
                //Debug.Log(charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)));
                //Debug.Log(this.transform.position);
                HitOrChangePos();
            }
        }
    }
}
