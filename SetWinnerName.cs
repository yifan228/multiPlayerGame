using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetWinnerName : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {

        gameObject.GetComponent<Text>().text = NotDes.instance.WInName;
    }

}
