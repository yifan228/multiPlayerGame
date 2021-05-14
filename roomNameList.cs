using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using Photon.Realtime;

public class roomNameList : MonoBehaviourPunCallbacks
{
    
    public GameObject roomNamePrefab;
    public Transform parentTrans;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
        foreach(RoomInfo room in roomList)
        {
            if (room.RemovedFromList)
            {
                DeleteRoom(room);

            }
            else
            {
                AddRoom(room);
            }
        }
        
    }

    void AddRoom(RoomInfo roomInfo)
    {
        GameObject obj = Instantiate(roomNamePrefab);
        obj.transform.SetParent(parentTrans);
        obj.GetComponentInChildren<Text>().text = roomInfo.Name;
    }

    void DeleteRoom(RoomInfo roomInfo)
    {
        int num = parentTrans.childCount;
        for(int i = 0; i < num; i++)
        {
            if(parentTrans.GetChild(i).GetComponentInChildren<Text>().text == roomInfo.Name)
            {
                Destroy(parentTrans.GetChild(i).gameObject);
            }
        }
    }
}
