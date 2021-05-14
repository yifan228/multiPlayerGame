using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmOpening : MonoBehaviour
{
    public GameObject openingStory;
    public GameObject LBsBtn;
    public GameObject RRsBtn;
    public GameObject reBtn;

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
