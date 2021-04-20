using System.Collections;
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
}
