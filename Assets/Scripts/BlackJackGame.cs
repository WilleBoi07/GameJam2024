using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UI;

public class BlackJackGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DealerCardtext;
    [SerializeField] private TextMeshProUGUI PlayerCardtext;
    [SerializeField] private TextMeshProUGUI BetValuetext;
    [SerializeField] private TextMeshProUGUI DealerCardValuetext;
    [SerializeField] private TextMeshProUGUI PlayerCardValuetext;

    string[] Deck = { "AoSp", "1oSp", "2oSp", "3oSp", "4oSp", "5oSp", "6oSp", "7oSp", "8oSp", "9oSp", "10oSp", "JoSp", "QoSp", "KoSp", "AoH", "2oH", "3oH", "4oH", "5oH", "6oH", "7oH", "8oH", "9oH", "10oH", "JoH", "QoH", "KoH", "AoC", "2oC", "3oC", "4oC", "5oC", "6oC", "7oC", "8oC", "9oC", "10oC", "JoC", "QoC", "KoC", "AoD", "2oD", "3oD", "4oD", "5oD", "6oD", "7oD", "8oD", "9oD", "10oD", "JoD", "QoD", "KoD" };
    List<string> DeckList = new List<string>();

    private List<string> playerHand = new List<string>();
    private List<string> dealerHand = new List<string>();
    private int playerHandValue = 0;
    private int dealerHandValue = 0;

    public Button Hit;
    public Button Stand;
    public Button Play;
    public Button Double;
    public Button Bet;

    private void Start()
    {
        DeckList.Clear(); // Clear the current deck
        DeckList.AddRange(Deck); // Populate with a standard deck

        // Set initial button states
        Hit.interactable = false;
        Stand.interactable = false;
        Double.interactable = false;
        Bet.interactable = false;
    }

    string DrawRandomCard()
    {
        int index = Random.Range(0, DeckList.Count);
        string card = DeckList[index];
        DeckList.RemoveAt(index);
        return card;
    }

    int GetCardValue(string card)
    {
        // The value of the card is the first part of the string
        string cardValue = card.Substring(0, card.IndexOf('o'));
        switch (cardValue)
        {
            case "K":
            case "Q":
            case "J":
                return 10;
            case "A":
                return 11; // Handle Aces as 11 initially
            default:
                return int.Parse(cardValue); // Convert number cards directly
        }
    }

    int CalculateHandValue(List<string> hand)
    {
        int total = 0;
        int aceCount = 0;

        foreach (string card in hand)
        {
            int cardValue = GetCardValue(card);
            total += cardValue;

            if (cardValue == 11) // If it's an Ace, keep track
                aceCount++;
        }

        // Adjust Aces from 11 to 1 if total exceeds 21
        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }

        return total;
    }

    public void PlayerHit()
    {
        string newCard = DrawRandomCard();
        playerHand.Add(newCard);
        PlayerCardtext.text += " " + newCard;

        // Recalculate player hand value and update UI
        playerHandValue = CalculateHandValue(playerHand);
        PlayerCardValuetext.text = "Value: " + playerHandValue;

        // Check if player is over 21
        if (playerHandValue > 21)
        {
            Hit.interactable = false;
            Stand.interactable = false;
            Double.interactable = false;
            Bet.interactable = false;
            Debug.Log("Player Busts!");
        }
    }

    public void PlayerStand()
    {
        Hit.interactable = false;
        Stand.interactable = false;
        Double.interactable = false;

        DealerTurn(); // Proceed with dealer's turn
    }

    public void PlayerDouble()
    {
        PlayerHit();
        if (playerHandValue <= 21)
        {
            PlayerStand();
        }
    }

    public void PlayerPlay()
    {
        Bet.interactable = true;
        Play.interactable = false;
    }

    public void PlayerBet()
    {
        Debug.Log("Play Button pressed");

        // Initial dealer and player cards
        string Dcard1 = DrawRandomCard();
        string Pcard1 = DrawRandomCard();
        string Pcard2 = DrawRandomCard();

        dealerHand.Add(Dcard1);
        playerHand.Add(Pcard1);
        playerHand.Add(Pcard2);

        DealerCardtext.text = "Cards: " + Dcard1;
        PlayerCardtext.text = "Cards: " + Pcard1 + " " + Pcard2;

        // Calculate initial hand values
        dealerHandValue = CalculateHandValue(dealerHand);
        playerHandValue = CalculateHandValue(playerHand);

        // Display initial values
        DealerCardValuetext.text = "Value: " + dealerHandValue;
        PlayerCardValuetext.text = "Value: " + playerHandValue;

        // Enable interaction for Hit, Stand, and Double
        Hit.interactable = true;
        Stand.interactable = true;
        Double.interactable = true;
        Bet.interactable = false;
    }

    public void DealerTurn()
    {
        while (dealerHandValue < 17) // Dealer hits until reaching at least 17
        {
            string newCard = DrawRandomCard();
            dealerHand.Add(newCard);
            DealerCardtext.text += " " + newCard;

            dealerHandValue = CalculateHandValue(dealerHand);
            DealerCardValuetext.text = "Value: " + dealerHandValue;
        }

        // Check if dealer busts or stands
        if (dealerHandValue > 21)
        {
            Debug.Log("Dealer Busts!");
        }
    }
}
