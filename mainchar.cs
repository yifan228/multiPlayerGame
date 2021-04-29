using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class mainchar : MonoBehaviourPun
{
    public GameObject playerCam;
    public Rigidbody2D target;
    public BoxCollider2D boxcolliderIsGround2D;
    public BoxCollider2D IsRihgtWall;
    public BoxCollider2D IsLeftWall;
    public SpriteRenderer mainchaRSprite;

   
    public static mainchar instance;

    public Text playerName;

    [SerializeField]
    private float runSpeed, jumpForce;

    public LayerMask platformlayerMask;

    //public float Hp;

    public bool DisableInputs = false;

    private void Awake()
    {
        if (photonView.IsMine)
        {
            instance = this;
            GameManager.instance.localPlayer = this.gameObject;
            playerName.text = PhotonNetwork.NickName;
            playerCam.SetActive(true);

        }
        else
        {
            playerName.text = photonView.Owner.NickName;
            playerName.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && DisableInputs==false)
        {
            checkInputs();
            
            //playerCam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y,-10f);
        }
        
    }

    private void checkInputs()
    {
        walk();
        camControll();
        
    }

    public void walk()
    {

        target.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, target.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && IsGround() == true)
        {
            target.AddForce(Vector2.up * jumpForce);
            //Debug.Log("jump");

        }

        if (target.velocity.y < 0f && IsGround() == false)
        {
            target.gravityScale = 1.5f;
            //Debug.Log("dropping");
        }
        else
        {
            target.gravityScale = 1f;
        }

        if (IsRightWallDet())
        {
            if (Input.GetAxisRaw("Horizontal")>0)
            {
                target.velocity = new Vector2(0, target.velocity.y);
            }
        }

        if (IsLeftWallDet())
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                target.velocity = new Vector2(0, target.velocity.y);
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

    [PunRPC]
    public void GameoverScene()
    {
        PhotonNetwork.LoadLevel("GameOver");

    }//declare gameover to everyone
}
