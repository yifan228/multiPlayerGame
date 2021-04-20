using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batSystem : MonoBehaviour
{
    public CircleCollider2D batRange;
    public bool canHit =false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.rigidbody.tag == "bullete")
        {
            canHit = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.rigidbody.tag == "bullete")
        {
            canHit = false;
        }
    }
}
