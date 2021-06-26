using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class globalLight : MonoBehaviour
{
    float time0;
    float TotTime;
    public Light2D GlobalLight;
    //bool IsDay = true;//control day or night
    int round =0;

    public int DayOrNite;//day1 nite-1
    public static globalLight instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
       time0 = Time.time;
       GlobalLight = GetComponent<Light2D>();
       
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (IsDay)
    //    {
    //        GlobalLight.intensity = (Time.time - time0) / 100;
    //        if (Time.time - time0 / 100 > 1.5)
    //        {
    //            IsDay = false;
    //        }
    //    }
    //    if (!IsDay)
    //    {
    //        GlobalLight.intensity = 300 - (Time.time - time0) / 100;
    //        if(300- (Time.time - time0) < 0)
    //        {
    //            time0 = Time.time;
    //            IsDay = true;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        //Debug.Log(TotTime);
        
        Count();
    }

    public void Count()
    {
        TotTime = Time.time - time0 -60f*round;
        if (TotTime > 60f)
        {
            round++;
            
        }

        if (TotTime > 27 && TotTime<=30f)
        {
            NiteToDay();
        }

        if(TotTime>57&& TotTime <= 60f)
        {
            DayToNite();
        }

    }

    public void DayToNite()
    {
        GlobalLight.intensity = 1-((TotTime - 57f)/3);
        if (GlobalLight.intensity < 0.1)
        {
            GlobalLight.intensity = 0;
            DayOrNite = -1;
        }
    }

    public void NiteToDay()
    {
        DayOrNite = 1;
        GlobalLight.intensity = (TotTime - 27f) / 3;
        if (GlobalLight.intensity > 0.9)
        {
            GlobalLight.intensity = 1;
        }
    }
}
