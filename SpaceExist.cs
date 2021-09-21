using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceExist : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Rigidbody2D>().gravityScale = 1f;
            collision.GetComponent<mainchar>().InSpace = false;
            collision.GetComponent<mainchar>().DisableInputs = false;
        }
    }
}
