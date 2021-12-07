using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Player
{
    public string _id;
    public string pseudo;
    public string classe;
    public string level;
    public string special;
    public string description;

    public void callTheToString()
    {
        Debug.Log("playerID : " + _id);
        Debug.Log("pseudo : " + pseudo);
        Debug.Log("type : " + classe);
        Debug.Log("level : " + level);
        Debug.Log("special : " + special);
        Debug.Log("description : " + description);
    }
}
