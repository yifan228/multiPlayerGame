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
        talkText.text = "";
    }

    public void shortcut1()
    {

        photonView.RPC("SendMsg", RpcTarget.AllBuffered, "NM屎L！");
    }

    public void shortcut2()
    {
        photonView.RPC("SendMsg", RpcTarget.AllBuffered, "別激動！");
    }

    public void shortcut3()
    {
        photonView.RPC("SendMsg", RpcTarget.AllBuffered, "AreWe屎黨了嗎?");
    }

    public void shortcut4()
    {
        photonView.RPC("SendMsg", RpcTarget.AllBuffered, "你玩的像坨屎╬");
    }

    public void shortcut5()
    {
        photonView.RPC("SendMsg", RpcTarget.AllBuffered, "屎恩羅天征"+"\n"+"ヽ(`Д´)ノ");
    }

    public void shortcut6()
    {
        photonView.RPC("SendMsg", RpcTarget.AllBuffered, "我在裝屎");
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
