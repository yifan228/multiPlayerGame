using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Throw : MonoBehaviourPun
{
    public GameObject Poo;

    public float launchForce;
    public float nowForce;

    private float startHoldDownTime;
    float HoldDownTime; 

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
        Vector2 mousePosition = playercamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.1f));
        direction = mousePosition - bowPosition;
        transform.up = direction;

        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startHoldDownTime = Time.time;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;

                //    Debug.Log(Input.mousePosition);
                //    Debug.Log(shootPoint.position);
                //    Debug.Log(bowPosition);
                //    Debug.Log(mousePosition);
                //    Debug.Log(direction);
            }

            if (Input.GetMouseButton(0))
            {
                HoldDownTime = Time.time - startHoldDownTime;
                nowForce = caclulateNowForce();
            }

            if (Input.GetMouseButtonUp(0))
            {
                HoldDownTime = Time.time - startHoldDownTime;
                nowForce = caclulateNowForce();
                shoot();
                turnOffspell();
            }
        }
        

        //if (playerMovement.instance.Hp == 0)
        //{
        //    destroyThisWeapon();
        //}
        if (photonView.IsMine)
        {
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = pointPosition(i * spaceBetweenPoints);
            }
        }
    }

    private float caclulateNowForce()
    {
        float maxHoldTime = 2f;
        float rate = Mathf.Clamp01(HoldDownTime / maxHoldTime);
        float force = launchForce * rate;
        return force;

    }    

    public void turnOffspell()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void shoot()
    {
        GameObject newPoo = PhotonNetwork.Instantiate(Poo.name, shootPoint.position, shootPoint.rotation);
        newPoo.GetComponent<Rigidbody2D>().velocity = transform.up * nowForce;
    }
    public Vector2 pointPosition(float t)
    {
        Vector2 position = (Vector2)shootPoint.position + direction.normalized * nowForce * t + 0.5f * Physics2D.gravity * t * t;

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
