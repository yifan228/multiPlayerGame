using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDes : MonoBehaviour
{
    public static NotDes instance;
    public string RoomName;
    public string MyName;
    public int myTeam;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    
}
