using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class globalLight : MonoBehaviour
{
    float time0;
    public Light2D GlobalLight;
    bool IsDay = true;//control day or night

    // Start is called before the first frame update
    void Start()
    {
        time0 = Time.time;
       GlobalLight = GetComponent<Light2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDay)
        {
            GlobalLight.intensity = (Time.time - time0) / 100;
            if (Time.time - time0 / 100 > 1.5)
            {
                IsDay = false;
            }
        }
        if (!IsDay)
        {
            GlobalLight.intensity = 300 - (Time.time - time0) / 100;
            if(300- (Time.time - time0) < 0)
            {
                time0 = Time.time;
                IsDay = true;
            }
        }
    }
}
