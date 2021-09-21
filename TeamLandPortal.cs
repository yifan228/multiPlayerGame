using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamLandPortal : MonoBehaviour
{
    public Transform Blue;
    public Transform Red;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "PlayerBattleBlue(Clone)"&&collision.GetComponent<PhotonView>().IsMine)
        {
            collision.transform.position = Blue.position;
        }else if(collision.name== "PlayerBattleRed(Clone)" && collision.GetComponent<PhotonView>().IsMine)
        {
            collision.transform.position = Red.position;
        }
    }
}
