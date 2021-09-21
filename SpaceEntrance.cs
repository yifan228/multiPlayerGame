using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 0f;
            collision.GetComponent<mainchar>().InSpace = true;
            collision.GetComponent<mainchar>().DisableInputs = true;
        }
    }
    
}
