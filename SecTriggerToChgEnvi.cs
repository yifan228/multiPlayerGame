using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SecTriggerToChgEnvi : MonoBehaviourPun
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.tag == "bullete" && collision.GetComponent<PhotonView>().IsMine)
        {
            SecChangeEnvi.instance.ActiveOrNot *= -1;
            StartCoroutine(DisapearTenSec());
        }
    }

    IEnumerator DisapearTenSec()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(true);
    }
   
}
