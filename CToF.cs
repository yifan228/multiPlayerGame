using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CToF : MonoBehaviourPun
{
    [SerializeField] Transform PosF;//
    public float stayTime = 2f;
    bool completeStay;

    Collider2D me;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine)
        {
            completeStay = true;
            me = collision;
            StartCoroutine(cout());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<PhotonView>().IsMine)
        {
            completeStay = false;
        }
    }
    IEnumerator cout()
    {
        yield return new WaitForSeconds(stayTime);

        if (completeStay)
        {

            me.transform.position = PosF.position;

            //photonView.RPC("MinusLocalPerson", RpcTarget.AllBuffered);
            //photonView.RPC("DestinationPerson", RpcTarget.AllBuffered);
            GameManager.instance.StepToWin(5);
            GameManager.instance.RespawnPoint.transform.position = PosF.transform.position;
        }
    }
    [PunRPC]
    private void MinusLocalPerson()
    {
        GameManager.instance.C -= 1;
    }
    [PunRPC]
    private void DestinationPerson()
    {
        GameManager.instance.F += 1;
    }//
}
