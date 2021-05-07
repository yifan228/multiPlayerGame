using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class shortcutOnOff : MonoBehaviourPun
{
    [SerializeField]private GameObject shortcutCanvas;
    int onoff = 1;

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.T) && onoff == 1)
            {
                shortcutCanvas.SetActive(true);
                onoff = onoff * -1;
            }
            else if (Input.GetKeyDown(KeyCode.T) && onoff == -1)
            {
                shortcutCanvas.SetActive(false);
                onoff = onoff * -1;
            }
        }
    }
}
