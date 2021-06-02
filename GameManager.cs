﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject canvas;
    public GameObject sceneCam;

    public Text spawnTimer ;

    public GameObject respawnUi;

    [HideInInspector]
    public GameObject localPlayer;

    public float timeAmount = 0;
    public bool startRespawn;
    public static GameManager instance = null;

    public GameObject statsCanvas;
    public int assistNum = 1;

    public Transform endingPoint;

    [SerializeField]private GameObject gameoverscipt;

    [Header("teamReSpawnPoint")]
    public GameObject teamRRResPoint;
    public GameObject teamLBResPoint;
    private void Awake()
    {
        
        instance = this;
        canvas.SetActive(true); 
    }

    private void Update()
    {
        if (startRespawn)
        {
            StartRespawn();
        }

        if (Input.GetKeyDown(KeyCode.Escape)&&assistNum ==1)
        {
            statsCanvas.SetActive(true);
            assistNum = assistNum * -1;
        } else if (Input.GetKeyDown(KeyCode.Escape)&&assistNum==-1)
        {
            statsCanvas.SetActive(false);
            assistNum = assistNum * -1;
        }
    }

    void StartRespawn()
    {
        timeAmount -= Time.deltaTime;
        spawnTimer.text = "revive in :" + timeAmount.ToString("F0");
        if (timeAmount <= 0)
        {
            respawnUi.SetActive(false);
            startRespawn = false;
            
            localPlayer.GetComponent<mainchar>().DisableInputs = false;
            localPlayer.GetComponent<PhotonView>().RPC("Revive", RpcTarget.AllBuffered);
            setRespawnLocation();
        }
    }

    public void enableRespawn()
    {
        timeAmount = 5;
        respawnUi.SetActive(true);
        startRespawn = true;
    }

    public void spawnPlayer()
    {
        float randomValue = Random.Range(-5, 5);
        PhotonNetwork.Instantiate(playerPrefab.name,new Vector2(playerPrefab.transform.position.x*randomValue,playerPrefab.transform.position.y),Quaternion.identity,0);
        canvas.SetActive(false);
        sceneCam.SetActive(false);
    }

    void setRespawnLocation()
    {
        if (TeamManager.instance.team == 0)
        {
            float rngPlace = Random.Range(0, 10);
            if (rngPlace <= 6f)
            {
                localPlayer.GetComponent<mainchar>().IsDef = false;
                float rng = Random.Range(-5, 5);
                localPlayer.transform.position = new Vector2(rng, 4f);
            }
            else
            {
                localPlayer.GetComponent<mainchar>().IsDef = true;
                localPlayer.transform.position = new Vector2(endingPoint.position.x + 3f, endingPoint.position.y - 2f);
            }
        }else if (TeamManager.instance.team == -1)
        {
            localPlayer.transform.position = teamLBResPoint.transform.position;
        }else if(TeamManager.instance.team == 1)
        {
            localPlayer.transform.position = teamRRResPoint.transform.position;
        }
        
    }

    public void leaveRoomBtn()
    {
        Destroy(NotDes.instance.gameObject);
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void BackToGame()
    {
        statsCanvas.SetActive(false);
    }
}
