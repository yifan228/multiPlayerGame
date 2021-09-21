using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamTransDoor : MonoBehaviour
{
    [SerializeField]Transform Tran;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
            collision.transform.position = Tran.position;
    }
}
