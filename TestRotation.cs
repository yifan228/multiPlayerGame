using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestRotation : MonoBehaviour
{
    float num = 0;
    private void FixedUpdate()
    {
        num += 40 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, num));
    }
}
