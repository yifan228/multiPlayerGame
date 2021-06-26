using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SecChangeEnvi : MonoBehaviourPun,IPunObservable
{
    public int ActiveOrNot=1;//active1 unactive2
    public static SecChangeEnvi instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (globalLight.instance.DayOrNite==-1)
        {
            if (ActiveOrNot == 1)
            {
                gameObject.SetActive(true);
            } else if (ActiveOrNot == -1)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ActiveOrNot);
        }

        if (stream.IsReading)
        {
            ActiveOrNot = (int) stream.ReceiveNext();
        }
    }
}
