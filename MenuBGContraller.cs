using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBGContraller : MonoBehaviour
{
    public GameObject Bg;
    public Transform InsPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Block")
        {
            Instantiate(Bg, InsPoint);
            Destroy(collision.gameObject);
        }
    }
}
