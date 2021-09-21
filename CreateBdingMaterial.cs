using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateBdingMaterial : MonoBehaviourPun
{
    [SerializeField] AudioSource shootSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && GetComponentInChildren<Throw>().spellAmount.fillAmount>0.4)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3 vec = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                GetComponentInChildren<Throw>().spellAmount.fillAmount -= 0.4f;
                switch (GameManager.instance.localPlayer.GetComponent<mainchar>().Stat)
                {
                    case "scissor":
                        PhotonNetwork.Instantiate("ScissorBlock", vec, Quaternion.identity);
                        photonView.RPC("ShootSoundPlay", RpcTarget.AllBuffered);
                        break;
                    case "stone":
                        PhotonNetwork.Instantiate("StoneBlock", vec, Quaternion.identity);
                        photonView.RPC("ShootSoundPlay", RpcTarget.AllBuffered);
                        break;

                    case "paper":
                        PhotonNetwork.Instantiate("Paperblock", vec, Quaternion.identity);
                        photonView.RPC("ShootSoundPlay", RpcTarget.AllBuffered);
                        break;
                    default: return;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 vec = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
                GetComponentInChildren<Throw>().spellAmount.fillAmount -= 0.4f;
                switch (GameManager.instance.localPlayer.GetComponent<mainchar>().Stat)
                {
                    case "scissor":
                        PhotonNetwork.Instantiate("ScissorBlock", vec, Quaternion.identity);
                        photonView.RPC("ShootSoundPlay", RpcTarget.AllBuffered);
                        break;
                    case "stone":
                        PhotonNetwork.Instantiate("StoneBlock", vec, Quaternion.identity);
                        photonView.RPC("ShootSoundPlay", RpcTarget.AllBuffered);
                        break;

                    case "paper":
                        PhotonNetwork.Instantiate("PaperBlock", vec, Quaternion.identity);
                        photonView.RPC("ShootSoundPlay", RpcTarget.AllBuffered);
                        break;
                    default: return;
                }
            }
        }
    }
    [PunRPC]
    public void ShootSoundPlay()
    {
        shootSound.Play();
    }
}
