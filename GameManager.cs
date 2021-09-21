using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPun
{
    public GameObject[] AllPlayers;

    public GameObject AspawnPoint;
    public GameObject RespawnPoint;//死亡儲存點

    public GameObject canvas;
    public GameObject sceneCam;

    public Text spawnTimer;

    public GameObject respawnUi;

    [HideInInspector]
    public GameObject localPlayer;

    public float timeAmount = 0;
    public bool startRespawn;
    public static GameManager instance;
    [Header("statCanvas")]
    public GameObject statsCanvas;
    [SerializeField] GameObject IllustrateCanvas;
    public int assistNum = 1;

    public Transform endingPoint;

    [SerializeField] private GameObject gameoverscipt;

    public Text Pin;

    [Header("teamReSpawnPoint")]
    public GameObject teamRRResPoint;
    public GameObject teamLBResPoint;

    
    int myPos, FstPos, SecPos, ThddPos;
    private string TransFormyPos(int num)
    {
        switch (num)
        {
            case 0:
                return "森屎界";
            case 1:
                return "地屎界";
            case 2:
                return "隱屎界";
            case 3:
                return "風屎界";
            case 4:
                return "宇宙界";
            case 5:
                return "冰屎界";
            
            default:
                return "";
        }
    }//transform mypos to string

    [Header("Battle")]
    public Transform endingStageEntrance;

    int[] myWay;
    int myIsPos;//目前在哪座島
    int myWayIndex = 0;//目前在第幾層
    bool AmIWin = false;
    public void StepToWin(int a)
    {
        if (myWay[myWayIndex] == a)
        {
            myWayIndex += 1;
            if (myWayIndex >= myWay.Length)
            {
                AmIWin = true;
            }
        }
    }

    public Transform AIsland, BIsland, CIsland, DIsland, EIsland, FIsland;
    public Text a, b, c, d, e, f, nowStep,stepMap;
    public int A, B, C, D, E, F;//amount of player in island;
    private void fillAmountText()
    {
        a.text = "A人數：" + $"{A}";
        b.text = "B人數：" + $"{B}";
        c.text = "C人數：" + $"{C}";
        d.text = "D人數：" + $"{D}";
        e.text = "E人數：" + $"{E}";
        f.text = "F人數：" + $"{F}";
    }

    int lastPositionNum;
    private int NextPos(int x)
    {
        int y = Random.Range(0, 6);
        while (x == y || (x + y) % 6 == 2 || (x + y) % 6 == 4 || (x + y) % 6 == 0||y==lastPositionNum)
        {
            y = Random.Range(0, 6);
        }
        lastPositionNum = x;
        return y;
    }

    //這裡不能用awake不然這個程式會隱藏，應該是stepmap還沒實例
    private void Start()
    {
        
        instance = this;
        
        canvas.SetActive(true);
        if (TeamManager.instance.team == 0)
        {
            myPos = 0;//目前在a
            FstPos = NextPos(myPos);
            SecPos = NextPos(FstPos);
            ThddPos = NextPos(SecPos);
            myWay = new int[] { FstPos, SecPos, ThddPos };
            stepMap.text = "第一我要："+"從森屎界到達"+TransFormyPos(myWay[0]) + "\n" +"第二我要：" + $"從{TransFormyPos(myWay[0])}到達" + TransFormyPos(myWay[1]) +"\n"+ "第三我要：" + $"從{TransFormyPos(myWay[1])}到達" + TransFormyPos(myWay[2]);
        }
        //Debug.Log(myPos);
        //Debug.Log(FstPos);
        //Debug.Log(SecPos);
        //Debug.Log(ThddPos);
    }

    private void Update()
    {
        Pin.text = " Ping:" + PhotonNetwork.GetPing();
        if (TeamManager.instance.team == 0 && myWayIndex < 3 &&nowStep!=null)
        {
            nowStep.text = "通過門的次數：" + $"{ myWayIndex }";
        }else if (TeamManager.instance.team == 0 && myWayIndex == 3 &&nowStep!=null)
        {
            nowStep.text = "目前位置：" + "痣慧之窟";
        }
        //if (Input.GetKeyDown(KeyCode.H) && TeamManager.instance.team == 0)
        //{
        //    photonView.RPC("SetBH", RpcTarget.AllBuffered);

            //}
        if ( AmIWin&&TeamManager.instance.team==0)
        {
            localPlayer.transform.position = endingStageEntrance.position;
            RespawnPoint.transform.position = endingStageEntrance.position;
            AmIWin = false;
        }

        //if (Input.GetKeyDown(KeyCode.C) && TeamManager.instance.team == 0)
        //{
        //    FindAllPlayer();
        //}// count player in each island

        //if(TeamManager.instance.team==0)
        //    fillAmountText();//show num of player in ui

        if (startRespawn)
        {
            StartRespawn();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && assistNum == 1)
        {
            statsCanvas.SetActive(true);
            assistNum = assistNum * -1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && assistNum == -1)
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
            localPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            localPlayer.GetComponentInChildren<Throw>().CanThrow = true;//能丟
        }
    }

    public void enableRespawn()
    {
        timeAmount = 5;
        respawnUi.SetActive(true);
        startRespawn = true;
        if (TeamManager.instance.team != 0)
        {
            localPlayer.GetComponentInChildren<Throw>().CanThrow = false;//不能丟東西
        }
    }

    public void spawnPlayer()
    {
        float randomValue = Random.Range(-1, 1);
        PhotonNetwork.Instantiate("Player",new Vector2(AspawnPoint.transform.position.x+randomValue,AspawnPoint.transform.position.y),Quaternion.identity,0);
        canvas.SetActive(false);
        sceneCam.SetActive(false);
        
        RespawnPoint.transform.position = AspawnPoint.transform.position;
    }

    //have not test
    void setRespawnLocation()
    {
        if (TeamManager.instance.team == 0)
        {
            float rngPlace = Random.Range(0, 10);
            if (rngPlace <= 10f)
            {
                //localPlayer.GetComponent<mainchar>().IsDef = false;
                float rng = Random.Range(-1, 1);
                localPlayer.transform.position = RespawnPoint.transform.position;//
            }
            else
            {
                //localPlayer.GetComponent<mainchar>().IsDef = true;
                localPlayer.transform.position = new Vector2(endingPoint.position.x, endingPoint.position.y);
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

    public void ClickIllustrateBtn()
    {
        IllustrateCanvas.SetActive(true);
    }

    public void BackToStat1()
    {
        IllustrateCanvas.SetActive(false);
    }

    [PunRPC]
    public void SetBH()
    {
        BHACon.instance.IsOpen = true;
        BHBCon.instance.IsOpen = true;
        BHCC.instance.IsOpen = true;
        BHDC.instance.IsOpen = true;
        BHEC.instance.IsOpen = true;
        BHFC.instance.IsOpen = true;
    }
    //[PunRPC]
    //public void APlus()
    //{
    //    A++;
    //}

    public void FindAllPlayer()
    {
        AllPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in AllPlayers)
        {
            WhitchIsland(player);
        }
    }
    
    public void WhitchIsland(GameObject player)
    {
        A = 0;
        B = 0;
        C = 0;
        D = 0;
        E = 0;
        F = 0;
        if (InIsland(player, AIsland))
        {
            A++;
        }else if(InIsland(player, BIsland))
        {
            B++;
        }else if(InIsland(player, CIsland))
        {
            C++;
        }else if (InIsland(player, DIsland))
        {
            D++;
        }else if(InIsland(player, EIsland))
        {
            E++;
        }else if(InIsland(player, FIsland))
        {
            F ++;
        }
    }

    private bool InIsland(GameObject player,Transform island,float rad = 60)
    {
        float num = (island.position.x - player.transform.position.x) * (island.position.x - player.transform.position.x) + (island.position.y - player.transform.position.y) * (island.position.y - player.transform.position.y) - rad * rad;
        return num <= 0;
    }

    
}
