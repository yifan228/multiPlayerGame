using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public Image fillImage;

   [PunRPC]
   public void HealthUpdate(float damage)
    {
        fillImage.fillAmount -= damage;
    }
  
}
