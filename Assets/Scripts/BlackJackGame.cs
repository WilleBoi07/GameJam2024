using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BlackJackGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI DealerCardtext;
    [SerializeField] private TextMeshProUGUI PlayerCardtext;
    [SerializeField] private TextMeshProUGUI BetValuetext;
    [SerializeField] private TextMeshProUGUI DealerCardValuetext;
    [SerializeField] private TextMeshProUGUI PlayerCardValuetext;
    [SerializeField] private BettingSystem BettingSystem;

    string[] Deck = { "AoSp", "2oSp", "3oSp", "4oSp", "5oSp", "6oSp", "7oSp", "8oSp", "9oSp", "10oSp", "JoSp", "QoSp", "KoSp", "AoH", "2oH", "3oH", "4oH", "5oH", "6oH", "7oH", "8oH", "9oH", "10oH", "JoH", "QoH", "KoH", "AoC", "2oC", "3oC", "4oC", "5oC", "6oC", "7oC", "8oC", "9oC", "10oC", "JoC", "QoC", "KoC", "AoD", "2oD", "3oD", "4oD", "5oD", "6oD", "7oD", "8oD", "9oD", "10oD", "JoD", "QoD", "KoD" };
    [SerializeField] Sprite[] cardSprites;
    List<Sprite> cardSpritesList = new List<Sprite>();
    List<string> DeckList = new List<string>();

    [SerializeField]
    CardToPoint playedCardPrefab;
    [SerializeField] RectTransform[] playerHandPositions;
    [SerializeField] RectTransform[] dealerHandPositions;
    int currentPlayerIndex = 0;
    int currentDealerIndex = 0;
    List<CardToPoint> createdCards = new List<CardToPoint>();

    private List<string> playerHand = new List<string>();
    private List<string> dealerHand = new List<string>();
    private int playerHandValue = 0;
    private int dealerHandValue = 0;
    int index;

    public Button Hit;
    public Button Stand;
    public Button Play;
    public Button Double;
    public Button Bet;
    public Button Chip1;
    public Button Chip5;
    public Button Chip10;
    public Button Chip25;
    public Button Chip50;
    public Button Chip100;
    public Button Chip500;
    public Button Chip1000;
    public Button Chip5000;
    public Button Chip10000;

    private void Start()
    {
        DeckList.Clear(); // Clear the current deck
        DeckList.AddRange(Deck); // Populate with a standard deck
        cardSpritesList.Clear();
        cardSpritesList.AddRange(cardSprites);

        // Set initial button states
        Hit.interactable = false;
        Stand.interactable = false;
        Double.interactable = false;
        Bet.interactable = false;
        Chip1.interactable = false;
        Chip5.interactable = false;
        Chip10.interactable = false;
        Chip50.interactable = false;
        Chip100.interactable = false;
        Chip500.interactable = false;
        Chip1000.interactable = false;
        Chip5000.interactable = false;
        Chip10000.interactable = false;
    }

    string DrawRandomCard()
    {
        index = Random.Range(0, DeckList.Count);
        string card = DeckList[index];
        DeckList.RemoveAt(index);
        return card;
    }

    void AddCardOnScreen(RectTransform target)
    {
        
       CardToPoint card =  Instantiate(playedCardPrefab, FindObjectOfType<Canvas>().transform).GetComponent<CardToPoint>();
       RectTransform cardTransfrom = card.GetComponent<RectTransform>();
        card.GetComponent<Image>().sprite = cardSpritesList[index];
        cardSpritesList.RemoveAt(index);
        cardTransfrom.position = new Vector3(100, 100, 0);//Lägger kortet på start positionen.
        card.target = target;
        card.shouldMove = true;
        createdCards.Add(card);
    }

    void RemoveAllCardsOnScreen()
    {
        for (int i = 0; i < createdCards.Count; i++)
        {
            Destroy(createdCards[i].gameObject);
        }
        createdCards.Clear();
        currentDealerIndex = 0;
        currentPlayerIndex = 0;
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
            Play.interactable = true;
            Debug.Log("Player Busts!");
            BettingSystem.LoseBet();
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
        PlayerStand();
        BettingSystem.DoubleBet();
    }

    public void PlayerPlay()
    {
        Bet.interactable = true;
        Chip1.interactable = true;
        Chip5.interactable = true;
        Chip10.interactable = true;
        Chip25.interactable = true;
        Chip50.interactable = true;
        Chip100.interactable = true;
        Chip500.interactable = true;
        Chip1000.interactable = true;
        Chip5000.interactable = true;
        Chip10000.interactable = true;
        Play.interactable = false;

        dealerHandValue = 0;
        playerHandValue = 0;

        DealerCardValuetext.text = "Value: " + dealerHandValue;
        PlayerCardValuetext.text = "Value: " + playerHandValue;

        DeckList.Clear(); // Clear the current deck
        DeckList.AddRange(Deck); // Populate with a standard deck
        playerHand.Clear();
        dealerHand.Clear();
        DealerCardtext.text = "Cards: ";
        PlayerCardtext.text = "Cards: ";
        RemoveAllCardsOnScreen();
        print("start new game");
    }

    public void PlayerBet()
    {
        Debug.Log("Bet Button pressed");

        // Initial dealer and player cards
        string Dcard1 = DrawRandomCard();
        AddCardOnScreen(dealerHandPositions[currentDealerIndex]);
        currentDealerIndex++;

        string Pcard1 = DrawRandomCard();
        AddCardOnScreen(playerHandPositions[currentPlayerIndex]);
        currentPlayerIndex++;
        string Pcard2 = DrawRandomCard();
        AddCardOnScreen(playerHandPositions[currentPlayerIndex]);
        currentPlayerIndex++;

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


        if (playerHandValue == 21)
        {
            Debug.Log("Player has a Blackjack!");
            BettingSystem.BlackjackWin();
            Play.interactable = true;
            return;
        }

        // Set buttons for the turn
        Hit.interactable = true;
        Stand.interactable = true;
        Double.interactable = true;
        Bet.interactable = false;
    }

    private void DealerTurn()
    {
        Hit.interactable = false;
        Stand.interactable = false;
        Double.interactable = false;
        Bet.interactable = false;
        Chip1.interactable = false;
        Chip5.interactable = false;
        Chip10.interactable = false;
        Chip25.interactable = false;
        Chip50.interactable = false;
        Chip100.interactable = false;
        Chip500.interactable = false;
        Chip1000.interactable = false;
        Chip5000.interactable = false;
        Chip10000.interactable = false;
        // Continue drawing until dealer has at least 17
        while (dealerHandValue < 17)
        {
            string newCard = DrawRandomCard();
            dealerHand.Add(newCard);
            DealerCardtext.text += " " + newCard;

            dealerHandValue = CalculateHandValue(dealerHand);
            DealerCardValuetext.text = "Value: " + dealerHandValue;
            Play.interactable = true;
        }

        if (dealerHandValue > 21 || playerHandValue > dealerHandValue)
        {
            Debug.Log("Player Wins!");
            BettingSystem.WinBet();
            Play.interactable = true;
        }
        else if (playerHandValue == dealerHandValue)
        {
            Debug.Log("Push (Draw)");
            BettingSystem.DrawBet();
            Play.interactable = true;
        }
        else
        {
            Debug.Log("Dealer Wins!");
            BettingSystem.LoseBet();
            Play.interactable = true;
        }
        Play.interactable = true;
    }
}
