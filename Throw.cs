using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Throw : MonoBehaviourPun
{
    public GameObject Poo;
    public float launchForce;
    public Transform shootPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    public Camera playercamera;

    public static Throw instance;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
           
                points[i] = Instantiate(point, shootPoint.position, Quaternion.identity);
    
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bowPosition = playercamera.ScreenToWorldPoint(shootPoint.position);
        Vector2 mousePosition = playercamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.1f));
        direction = mousePosition - bowPosition;
        transform.up = direction;

        if (photonView.IsMine)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = pointPosition(i * spaceBetweenPoints);
            }
        }
            
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled=true;
            shoot();
            Invoke("turnOffspell", 0.1f);
            //    Debug.Log(Input.mousePosition);
            //    Debug.Log(shootPoint.position);
            //    Debug.Log(bowPosition);
            //    Debug.Log(mousePosition);
            //    Debug.Log(direction);
            }

            //if (playerMovement.instance.Hp == 0)
            //{
            //    destroyThisWeapon();
            //}
        }
    public void turnOffspell()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void shoot()
    {
        GameObject newPoo = PhotonNetwork.Instantiate(Poo.name, shootPoint.position, shootPoint.rotation);
        newPoo.GetComponent<Rigidbody2D>().velocity = transform.up * launchForce;
    }
    public Vector2 pointPosition(float t)
    {
        Vector2 position = (Vector2)shootPoint.position + direction.normalized * launchForce * t + 0.5f * Physics2D.gravity * t * t;

        return position;

    }
    //public void destroyThisWeapon()
    //{
    //    Destroy(gameObject);
    //}
    //public void hideThisWeapon()
    //{
    //    gameObject.GetComponent<SpriteRenderer>().enabled = false;
    //}
}
