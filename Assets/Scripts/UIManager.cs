using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabCarte;
    List<Player> playersUI;
    List<GameObject> cards;

    [SerializeField]
    GameObject cardHand;

    private void Start()
    {
        playersUI = new List<Player>();
        cards = new List<GameObject>();
    }

    public void UpdateUI(ref List<Player> players)
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
            var card = Instantiate<GameObject>(prefabCarte, cardHand.transform);
            cards.Add(card); // to modify
            card.GetComponent<Card>().playerIndex = players.IndexOf(player);
        }

        UpdateUICardsValues();
        UpdateUICardsPositions();
    }


    private void UpdateUICardsValues()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.Find("Niveau").GetComponent<Text>().text = playersUI[i].level;
            cards[i].transform.Find("NomCarte").GetComponent<Text>().text = playersUI[i].pseudo;
            cards[i].transform.Find("PointsDeVie").GetComponent<Text>().text = "3";
            cards[i].transform.Find("Dégâts").GetComponent<Text>().text = playersUI[i].special;

        }
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

    public void ClickPlayer(int index)
    {


    }
}
