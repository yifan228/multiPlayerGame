using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class talkManager: MonoBehaviourPun,IPunObservable
{
    public GameObject TalkBubble;
   
    public Text talkText;

    [HideInInspector]public InputField talkInputField;


    //private bool disableSend;

    private void Awake()
    {
        talkInputField = GameObject.Find("talkInput").GetComponent<InputField>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (talkInputField.isFocused)
            {
                Throw.instance.isTalking();
                
                if (talkInputField.text!="" && Input.GetKeyDown(KeyCode.Tab))
                {
                    
                    photonView.RPC("SendMsg", RpcTarget.AllBuffered,talkInputField.text);
                    talkInputField.text = "";
                }

            }
            else
            {
                Throw.instance.isNotTalking();
            }
        }
        
    }

    [PunRPC]
    void SendMsg(string msg)
    {
        talkText.text = msg;
        TalkBubble.SetActive(true);
        StartCoroutine(hideTalkBubble());
    }
    IEnumerator hideTalkBubble()
    {
        yield return new WaitForSeconds(3f);
        TalkBubble.SetActive(false);
        
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(TalkBubble.activeSelf);
        }
        else if (stream.IsReading)
        {
            TalkBubble.SetActive((bool)stream.ReceiveNext());
        }
    }
}
