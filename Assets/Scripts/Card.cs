using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    public int playerIndex;
    private Button _hiddenButton;
    DatabaseAccess DB;

    // Start is called before the first frame update
    void Start()
    {
        _hiddenButton = gameObject.transform.Find("HiddenButton").GetComponent<Button>();
        _hiddenButton.onClick.AddListener(ClickCard);
        DB = GameObject.Find("DataBase").GetComponent<DatabaseAccess>();
    }

    private void ClickCard()
    {
        //Debug.Log(playerIndex);
        Player p = DB.listPlayers[playerIndex];
        p.level = (int.Parse(p.level)+1).ToString();
        DB.UpdatePlayer(p);
        DB.GetPlayersInfos();
        DB.externUpdatePlayer0Name();
    }

    private void OnDestroy()
    {
        _hiddenButton.onClick.RemoveAllListeners();
    }

}
