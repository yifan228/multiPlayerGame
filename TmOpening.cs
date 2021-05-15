using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class TmOpening : MonoBehaviour
{
    public GameObject openingStory;
    public GameObject LBsBtn;
    public GameObject RRsBtn;
    public GameObject reBtn;
    public GameObject StartBtn;

    public GameObject music;

    private void Start()
    {
        Invoke("Set", 8f);
        
    }

    public void Set()
    {
        openingStory.SetActive(false);
        LBsBtn.SetActive(true);
        RRsBtn.SetActive(true);
        reBtn.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            StartBtn.SetActive(true);
        }
    }

    public void setMus()
    {
        music.SetActive(true);
    }

    public void setMusF()
    {
        music.SetActive(false);
    }
}
