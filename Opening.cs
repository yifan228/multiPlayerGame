using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening : MonoBehaviour
{
    public GameObject openingStory;
    public GameObject startBtn;
    public GameObject music;

    private void Start()
    {
        Invoke("Set", 8f);

    }

    public void Set()
    {
        openingStory.SetActive(false);
        startBtn.SetActive(true);
    }

    public void setMus()
    {
        music.SetActive(true);
    }
}
