using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDes : MonoBehaviour
{
    public static NotDes instance;
    public string RoomName;
    public string MyName;
    public int myTeam;
    public string WInName;
    public int WinTeam;
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

}
