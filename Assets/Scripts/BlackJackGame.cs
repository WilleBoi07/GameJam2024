using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackJackGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DealerCardtext;
    [SerializeField] private TextMeshProUGUI PlayerCardtext;

    string[] Deck = { "JokerRed", "JokerBlack", "AoSp", "1oSp", "2oSp", "3oSp", "4oSp", "5oSp", "6oSp", "7oSp", "8oSp", "9oSp", "10oSp", "JoSp", "QoSp", "KoSp", "AoH", "2oH", "3oH", "4oH", "5oH", "6oH", "7oH", "8oH", "9oH", "10oH", "JoH", "QoH", "KoH", "AoC", "2oC", "3oC", "4oC", "5oC", "6oC", "7oC", "8oC", "9oC", "10oC", "JoC", "QoC", "KoC", "AoD", "2oD", "3oD", "4oD", "5oD", "6oD", "7oD", "8oD", "9oD", "10oD", "JoD", "QoD", "KoD" };
    List<string> DeckList = new List<string>();

    private void Start()
    {
        DeckList.RemoveRange(0, DeckList.Count);//Tömmer nuvarande kortlek
        DeckList.AddRange(Deck); //lägger in en standard kortlek i nuvarande kortlek
    }

    string DrawRandomCard()
    {
        string card;
        int index = Random.Range(0, DeckList.Count);
        card = DeckList[index];
        DeckList.RemoveAt(index);
        return card;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("void updating");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            //int card = 70;
            string Dcard1 = DrawRandomCard();
            string Dcard2 = DrawRandomCard();
            string Pcard1 = DrawRandomCard();
            string Pcard2 = DrawRandomCard();
           // card = UnityEngine.Random.Range(0, Deck.Length);
            
/*
            while(card == Dcard1)
            {
                Debug.Log("Cards shuffleing 1");
                card = UnityEngine.Random.Range(0, Deck.Length);
                Dcard1 = card;
            }
            while (card == Dcard2)
            {
                Debug.Log("Cards shuffleing 2");
                card = UnityEngine.Random.Range(0, Deck.Length);
                Dcard2 = card;
            }
            while (card == Pcard1)
            {
                Debug.Log("Cards shuffleing 3");
                card = UnityEngine.Random.Range(0, Deck.Length);
                Pcard1 = card;
            }
            while (card == Pcard1)
            {
                Debug.Log("Cards shuffleing 4");
                card = UnityEngine.Random.Range(0, Deck.Length);
                Pcard1 = card;
            }*/
            Debug.Log("Card texts set");
            DealerCardtext.text = ("Cards: " + Dcard1 + Dcard2);
            PlayerCardtext.text = ("Cards: " + Pcard1 + Pcard2);
        }
    }
}
