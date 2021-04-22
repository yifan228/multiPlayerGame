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
    public bool Ishit;

    public float BatForce = 500;
    private Rigidbody2D Goal;
    
    public static batSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullete")
        {
            Goal = col.GetComponent<Rigidbody2D>();
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

    void hitting()
    {
        if (Ishit && canHit)
        {
            

                Vector2 mousePosition = charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                Vector2 batPosition = this.transform.position;
                HitForceVector = (mousePosition - batPosition) * BatForce;
                Goal.AddForceAtPosition(HitForceVector, Goal.transform.position, ForceMode2D.Impulse);;
                Debug.Log("hitting");
                Debug.Log(HitForceVector);

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

                        Debug.Log("cameraDetectCanHit");
                        Ishit = true;
                    }
                    else
                    {
                        Debug.Log("whatUp");
                        Ishit = false;
                    }

                }

                Debug.Log(hit.collider.name);
                //Debug.Log(Input.mousePosition);
                //Debug.Log(charcamera.ScreenToWorldPoint(Input.mousePosition+ charcamera.WorldToScreenPoint(charcamera.transform.position)));
                //Debug.Log(charcamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)));
                //Debug.Log(this.transform.position);
                hitting();
            }
        }
    }
}
