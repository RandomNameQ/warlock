using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public static class WhoIsOwner
{
    public static void DisplayComponents(PlayerStats obj)
    {
        Debug.Log("_________________________");
        Debug.Log("Player"+obj.OwnerId+" "+obj.IsOwner +" хоязин "+ obj.GetType().ToString()+" скрипта");
        Debug.Log("Player"+obj.OwnerId+" "+obj.NetworkObject.IsOwner+" хозяин "+ obj.gameObject.name +" игрового обьекта");
        Debug.Log("_________________________");

    }

    
}
