using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class mainchar : MonoBehaviourPunCallbacks
{
    public GameObject playerCam;
    public Rigidbody2D target;
    //private CharacterController targetController; 這個東西跟rigidbody不能一起用
    public float AddVelocityByEffector=0;
    private bool inSpace=false;
    public bool InSpace { get => inSpace; set => inSpace = value; }

    public bool IsOnEffector;

    private Vector2 OriginalVelocity;
    public BoxCollider2D boxcolliderIsGround2D;
    public BoxCollider2D IsRihgtWall;
    public BoxCollider2D IsLeftWall;
    public SpriteRenderer mainchaRSprite;
    public GameObject Lit;
    public int LitOn=1;

    [SerializeField]
    //public bool IsDef = false;//搞人的玩家
    [Header("剪刀石頭布")]
    public Image CDImg;//冷卻icon
    public string Stat;//剪刀1石頭2布3
    public Image StatImage;
    public Sprite scissorsImg;
    public Sprite stoneImg;
    public Sprite paperImg;
    bool CanChangeStat=true;
    float countDownNum=3;

    //public int IsTeamBlueRedTeam = 0;//0 is not Team,1 is blue,-1 is red
    public Color myTeamColor;

    public static mainchar instance;

    public Text playerName;
    public string playerMasterName;//used to gameOverManager get winnerName

    [SerializeField]
    private float runSpeed;
    
    float jumpForce=280f;

    public LayerMask platformlayerMask;

    //public float Hp;

    public bool DisableInputs = false;

    public int teamNum;
    public AudioSource jumpSound;



    //[Header("魔法傳送門")]
    //public GameObject trandoor;//被打的時候生成的傳送門
    //Transform trDoorPos;

    private void Start()
    {
        //targetController = gameObject.GetComponent<CharacterController>();
        
        if (photonView.IsMine && TeamManager.instance.team ==0)
        {
            instance = this;
                
            GameManager.instance.localPlayer = this.gameObject;
            
            playerName.text = PhotonNetwork.NickName;
            //playerMasterName = PhotonNetwork.NickName;
            playerCam.SetActive(true);
           
        }
        else if (photonView.IsMine && TeamManager.instance.team == -1)
        {
            
            instance = this;
            GameManager.instance.localPlayer = this.gameObject;
            //TeamManager.instance.localPlayer = this.gameObject;
            //playerMasterName = PhotonNetwork.NickName;
            playerName.text = PhotonNetwork.NickName;
            playerCam.SetActive(true);
            

        }
        else if (photonView.IsMine && TeamManager.instance.team == 1)
        {
            instance = this;
            GameManager.instance.localPlayer = this.gameObject;
            //TeamManager.instance.localPlayer = this.gameObject;
            playerName.text = PhotonNetwork.NickName;
            //playerMasterName = PhotonNetwork.NickName;
            playerCam.SetActive(true);
            
        }
        else if(!photonView.IsMine){
            if (TeamManager.instance.team == 0&&NotDes.instance.myTeam==0)//在teammanager還未生成，剛進入選對模式時，看到的人物顏色正確
            {
                //target.isKinematic = true;//ignore collide 
                playerName.text = photonView.Owner.NickName;
                playerName.color = Color.red;
            }else
            {
                playerName.text = photonView.Owner.NickName;
            }
        }
    }

    [PunRPC]
    public void SetStatImageToScissor()
    {
        StatImage.sprite = scissorsImg;
    }

    [PunRPC]
    public void SetStatImageToStone()
    {
        StatImage.sprite = stoneImg;
    }

    [PunRPC]
    public void SetStatImageToPaper()
    {
        StatImage.sprite = paperImg;
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.F))
        //{
        //    target.velocity = new Vector2(target.velocity.x,5f);
        //}
        if (TeamManager.instance.team != 0 && photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && CanChangeStat)
            {
                Stat = "scissor";
                photonView.RPC("SetStatImageToScissor", RpcTarget.AllBuffered);
                CanChangeStat = false;
                CDImg.fillAmount = 1;
                StartCoroutine(CountDown(true));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && CanChangeStat)
            {

                Stat = "stone";
                photonView.RPC("SetStatImageToStone", RpcTarget.AllBuffered);
                CanChangeStat = false;
                CDImg.fillAmount = 1;
                StartCoroutine(CountDown(true));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && CanChangeStat)
            {

                Stat = "paper";
                photonView.RPC("SetStatImageToPaper", RpcTarget.AllBuffered);
                CanChangeStat = false;
                CDImg.fillAmount = 1;
                StartCoroutine(CountDown(true));
            }
        }

        if(TeamManager.instance.team == 0 && photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.L)&&LitOn==1)
            {
                photonView.RPC("LitON", RpcTarget.AllBuffered);
                LitOn *= -1;
            }else if(Input.GetKeyDown(KeyCode.L) && LitOn == -1)
            {
                photonView.RPC("LitOff", RpcTarget.AllBuffered);
                LitOn *= -1;
            }
            
        }

    }

    [PunRPC]
    public void LitON()
    {
        Lit.SetActive(true);
    }

    [PunRPC]
    public void LitOff()
    {
        Lit.SetActive(false);
    }

    IEnumerator CountDown(bool count)
    {
        while (count)
        {
            countDownNum -= Time.deltaTime;
            CDImg.fillAmount = countDownNum / 3f;//3是冷卻時間
            //Debug.Log(countDownNum);
            yield return null;
            if (countDownNum <= 0)
            {
                CanChangeStat = true;
                countDownNum = 3;
                count =false;
                CDImg.fillAmount = 0;
            }
        }
    }
    

    //FixedUpdate
    void FixedUpdate()
    {
        if (photonView.IsMine && DisableInputs==false)
        {
            checkInputs();
            
            //playerCam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y,-10f);
            teamNum = TeamManager.instance.team; 
        }
        //let gameovertrigger know whose team win;
    }

    

    private void checkInputs()
    {
        walk();
        camControll();
    }

    public void walk()
    {
        
        if (TeamManager.instance.team == 0)
        {
           
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)||IsOnEffector)
                target.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed+AddVelocityByEffector, target.velocity.y);//如果只有這行的話，角色的速度永遠只會受到getaxisraw的影響，不會出現慣性，也無法被effector賦予速度
            
        }
        else
        {
            target.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, target.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGround() == true)
        {
            target.AddForce(Vector2.up * jumpForce);
            //Debug.Log("jump");
            photonView.RPC("JumpSoundPlay", RpcTarget.AllBuffered);

        }

        if (target.velocity.y < 0f && IsGround() == false && !inSpace)
        {
            target.gravityScale = 1.5f;
            //Debug.Log("dropping");
        }
        else if(!inSpace)
        {
            target.gravityScale = 1f;
        }

        if (IsRightWallDet())
        {
            if (Input.GetAxisRaw("Horizontal")>0)
            {
                target.velocity = new Vector2(0+AddVelocityByEffector, target.velocity.y);
            }
        }

        if (IsLeftWallDet())
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                target.velocity = new Vector2(0+AddVelocityByEffector, target.velocity.y);
            }
        }

        if (target.velocity.x < -0.1f && Input.GetAxisRaw("Horizontal")<-0.1f)
        {
            photonView.RPC("MainCharSpriteXTrue", RpcTarget.AllBuffered);
            
        }
        else if (target.velocity.x > 0.1f && Input.GetAxisRaw("Horizontal") > -0.1f)
        {
            photonView.RPC("MainCharSpriteXFalse", RpcTarget.AllBuffered);
        }
        else
        {
            mainchaRSprite.flipX = mainchaRSprite.flipX;
        }
    }

    public bool IsGround()
    {
        float extraHeight = 0.1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcolliderIsGround2D.bounds.center, boxcolliderIsGround2D.bounds.size, 0f, Vector2.down, extraHeight, platformlayerMask);
        //Debug.Log(raycastHit.collider);        
        return raycastHit.collider != null;
    }

    public bool IsRightWallDet()
    {
        float extraHeight = 0.1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(IsRihgtWall.bounds.center,IsRihgtWall.bounds.size, 0f, Vector2.right, extraHeight, platformlayerMask);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    public bool IsLeftWallDet()
    {
        float extraHeight = 0.1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(IsLeftWall.bounds.center,IsLeftWall.bounds.size, 0f, Vector2.left, extraHeight, platformlayerMask);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    private void camControll()
    {
        //playerCam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
        playerCam.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    [PunRPC]
    private void MainCharSpriteXFalse()
    {
        mainchaRSprite.flipX = false;
    }

    [PunRPC]
    private void MainCharSpriteXTrue()
    {
        mainchaRSprite.flipX = true;
    }

    public void setPosition(Vector3 position)
    {
       target.transform.position = position;
    }

    public void setVelocity(Vector2 velocity)
    {
        target.velocity = velocity;
    }

    
    //[PunRPC]
    //public void BeHitten(Vector2 addPosition)
    //{
    //    if (photonView.IsMine)
    //    {
    //        Vector3 pos = new Vector3(target.transform.position.x+addPosition.x, target.transform.position.y + addPosition.y, target.transform.position.z);
    //        trDoorPos.position = pos;
    //        StartCoroutine(Disapear());
    //        target.transform.position = pos;
    //    }

    //}//using AddForce will render a wierd phenomanon,using velocity either

    //IEnumerator Disapear()
    //{
    //    GameObject obj = Instantiate(trandoor, trDoorPos);
    //    yield return null;
    //    yield return new WaitForSeconds(1);
    //    Destroy(obj);
    //}

    [PunRPC]
    public void JumpSoundPlay()
    {
        jumpSound.Play();
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
            
    //        stream.SendNext(IsDef);
    //        //stream.SendNext(transform.position);
    //        //if (TeamManager.instance.team != 0)
    //        //{
    //        //    stream.SendNext(playerName.color);
    //        //}
    //        //stream.SendNext(playerMasterName);

    //    }
    //    else if (stream.IsReading)
    //    {
           
    //        IsDef = (bool)stream.ReceiveNext();
    //        //transform.position = (Vector3)stream.ReceiveNext();
    //        //if (TeamManager.instance.team != 0)
    //        //{
    //        //    playerName.color = (Color)stream.ReceiveNext();
    //        //}
    //        //playerMasterName = (string)stream.ReceiveNext();
    //    }
    //}


    [PunRPC]//選對時用的
    public void Des()
    {
        //gameObject.SetActive(false);
        PhotonNetwork.Destroy(this.gameObject);
    }
    //[PunRPC]
    //public void Appear()
    //{
    //    gameObject.SetActive(true);
    //}
}

