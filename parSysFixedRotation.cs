using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parSysFixedRotation : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
    }
}
