﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Photon.Pun;

public class mixerContral : MonoBehaviourPun
{
    bool isfinding;
    public AudioSource BGM1;
    //public AudioSource BGM2;
    //public AudioSource BGM3;
    //public AudioSource BGM4;
    public AudioSource SE1;
    public AudioSource SE2;
    public AudioSource SE3;
    GameObject player;
    GameObject FindWeapon;
    GameObject FindbatRange;
    GameObject[] players;

    //private void Start()
    //{
    //    Invoke("FindPlayer", 10f);


    //    //SE1 = player.GetComponent<AudioSource>();

    //}

    

    private void Update()
    {
        if (isfinding)
        {


            
            if (GameManager.instance.localPlayer != null)
            {
                player = GameManager.instance.localPlayer;
                SE1 = player.GetComponent<AudioSource>();
                    
            }
            
            //} else if (TeamManager.instance.team == -1)
            //{
            //    if (GameObject.Find("PlayerBattleBlue(Clone)") != null)
            //    {
            //        //player = GameObject.Find("PlayerBattleBlue(Clone)");
            //        SE1 = player.GetComponent<AudioSource>();
            //        isfinding = false;
            //    }
            //} else if (TeamManager.instance.team == 1)
            //{
            //    if (GameObject.Find("PlayerBattleRed(Clone)") != null)
            //    {
            //        //player = GameObject.Find("PlayerBattleRed(Clone)");
            //        SE1 = player.GetComponent<AudioSource>();
            //        isfinding = false;
            //    }
            //}

            if (player != null)
            {
                FindWeapon = GameObject.Find("weapon");
                SE2 = FindWeapon.GetComponent<AudioSource>();

            }
            if (player != null)
            {
                FindbatRange = GameObject.Find("batRange");
                SE3 = FindbatRange.GetComponent<AudioSource>();
                isfinding = false;
            
            }
        }
    }
    
    //public void setMasterVolume(float vol)
    //{
    //    mixer.SetFloat("MasterVolume", vol);
    //}用mixer的話還需要研究photon的event東西（可能要對接一些spi才能用不然會出現RaiseEvent(200) failed. Your event is not being sent! Check if your are in a Room），懶懶病發作

    public void setBGVolume(float vol)
    { 
        BGM1.volume = vol;
        //BGM2.volume = vol;
        //BGM3.volume = vol;
        //BGM4.volume = vol;
        //Debug.Log(vol);
    }

    public void setSEVolume(float vol)
    {
        SE1.volume = vol;
        SE2.volume = vol;
        SE3.volume = vol;
    }

    //[PunRPC]
    //public void SetBGMVol()
    //{
        
    //}
}
