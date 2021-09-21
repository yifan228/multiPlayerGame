using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffector : MonoBehaviour
{
    public float AddVelocity=2;
    //private float graduateNum=0.1f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if( collision.tag == "Bullete")
        //{
        //    collision.GetComponent<Rigidbody2D>().velocity += new Vector2(AddVelocity*graduateNum,0f);
        //}else
        if (collision.tag == "Player")
        {   if (collision.GetComponent<mainchar>().IsGround())
            {
                collision.GetComponent<mainchar>().IsOnEffector = true;
                collision.GetComponent<mainchar>().AddVelocityByEffector = AddVelocity;
            }else {
                collision.GetComponent<mainchar>().IsOnEffector = false;
                collision.GetComponent<mainchar>().AddVelocityByEffector = 0f;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<mainchar>().IsOnEffector = false;
            collision.GetComponent<mainchar>().AddVelocityByEffector = 0f;
        }
    }
}
