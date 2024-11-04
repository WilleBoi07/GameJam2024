using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackJack : MonoBehaviour
{

    string[] Deck = { "AoSp", "1oSp", "2oSp", "3oSp", "4oSp", "5oSp", "6oSp", "7oSp", "8oSp", "9oSp", "10oSp", "JoSp", "QoSp", "KoSp", "AoH", "2oH", "3oH", "4oH", "5oH", "6oH", "7oH", "8oH", "9oH", "10oH", "JoH", "QoH", "KoH", "AoC", "2oC", "3oC", "4oC", "5oC", "6oC", "7oC", "8oC", "9oC", "10oC", "JoC", "QoC", "KoC","AoD", "2oD", "3oD", "4oD", "5oD", "6oD", "7oD", "8oD", "9oD", "10oD", "JoD", "QoD", "KoD" };

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 4; i++)
            {
                int card = 0;
                card = UnityEngine.Random.Range(0, Deck.Length);
                Debug.Log(Deck[card]);
            }
        }        
    }
}
