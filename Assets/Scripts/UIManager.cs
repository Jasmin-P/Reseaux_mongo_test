using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabCarte;
    List<Player> playersUI;
    List<GameObject> cards;

    private void Start()
    {
        playersUI = new List<Player>();
        cards = new List<GameObject>();
    }

    public void UpdateUI(List<Player> players)
    {
        playersUI.Clear();
        foreach (GameObject var in cards)
        {
            Destroy(var);
        }
        cards.Clear();

        //if new player then add new UIplayer
        foreach (var player in players)
        {           
            playersUI.Add(player);
            cards.Add(Instantiate<GameObject>(prefabCarte)); // to modify           
        }

        UpdateUICardsValues();
        UpdateUICardsPositions();

        Debug.Log(cards.Count);
    }


    private void UpdateUICardsValues()
    {
    }

    private void UpdateUICardsPositions()
    {
        float pos = 0;
        foreach (var card in cards)
        {            
            card.transform.position = Vector3.zero + new Vector3(pos * 18 / cards.Count - 8, 0, 0);
            pos += 1;
        }
    }
}
