using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//building materials
public class CreatedBlock : MonoBehaviourPun
{
    //public bool IsCrash =false;
    
    //public void SetBool(bool isCrash)
    //{
    //    this.IsCrash = isCrash;
    //}
    public string Stat = "";

    // Update is called once per frame

    private void Start()
    {
        Stat = GameManager.instance.localPlayer.GetComponent<mainchar>().Stat;
    }
    //void Update()
    //{
    //    if (IsCrash)
    //    {
    //        Destroy(gameObject);
    //    }


    //}
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(IsCrash);
    //    }else if (stream.IsReading)
    //    {
    //        IsCrash = (bool)stream.ReceiveNext();
    //    }
    //}
    [PunRPC]
    void Des()
    {
        Destroy(gameObject);
    }
}
