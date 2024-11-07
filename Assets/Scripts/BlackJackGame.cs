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
    [SerializeField] private TextMeshProUGUI GameResultText;
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
        Hit.gameObject.SetActive(false);
        Stand.gameObject.SetActive(false);
        Double.gameObject.SetActive(false);
        Bet.gameObject.SetActive(false);
        Chip1.gameObject.SetActive(false);
        Chip5.gameObject.SetActive(false);
        Chip10.gameObject.SetActive(false);
        Chip25.gameObject.SetActive(false);
        Chip50.gameObject.SetActive(false);
        Chip100.gameObject.SetActive(false);
        Chip500.gameObject.SetActive(false);
        Chip1000.gameObject.SetActive(false);
        Chip5000.gameObject.SetActive(false);
        Chip10000.gameObject.SetActive(false);
        GameResultText.gameObject.SetActive(false);
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
        createdCards.Add(card);
        RectTransform cardTransfrom = card.GetComponent<RectTransform>();
        card.GetComponent<Image>().sprite = cardSpritesList[index];
        cardSpritesList.RemoveAt(index);
        cardTransfrom.position = new Vector3(980, 440, 0);//Lägger kortet på start positionen.
        card.target = target;
        card.shouldMove = true;
        
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
        cardSpritesList.Clear();
        cardSpritesList.AddRange(cardSprites);
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
        AddCardOnScreen(playerHandPositions[currentPlayerIndex]);
        currentPlayerIndex++;
        playerHand.Add(newCard);
        PlayerCardtext.text += " " + newCard;

        // Recalculate player hand value and update UI
        playerHandValue = CalculateHandValue(playerHand);
        PlayerCardValuetext.text = "Value: " + playerHandValue;

        // Check if player is over 21
        if (playerHandValue > 21)
        {
            Hit.gameObject.SetActive(false);
            Stand.gameObject.SetActive(false);
            Double.gameObject.SetActive(false);
            Bet.gameObject.SetActive(false);
            Play.gameObject.SetActive(true);
            GameResultText.text = "Player busts";
            GameResultText.gameObject.SetActive(true);
            BettingSystem.LoseBet();
        }
    }

    public void PlayerStand()
    {
        Hit.gameObject.SetActive(false);
        Stand.gameObject.SetActive(false);
        Double.gameObject.SetActive(false);

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
        Bet.gameObject.SetActive(true);
        Chip1.gameObject.SetActive(true);
        Chip5.gameObject.SetActive(true);
        Chip10.gameObject.SetActive(true);
        Chip25.gameObject.SetActive(true);
        Chip50.gameObject.SetActive(true);
        Chip100.gameObject.SetActive(true);
        Chip500.gameObject.SetActive(true);
        Chip1000.gameObject.SetActive(true);
        Chip5000.gameObject.SetActive(true);
        Chip10000.gameObject.SetActive(true);
        Play.gameObject.SetActive(false);
        GameResultText.gameObject.SetActive(false);

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
            BettingSystem.BlackjackWin();
            GameResultText.text = "BlackJack! you win: " + BettingSystem.currentBet * 2.5;
            GameResultText.gameObject.SetActive(true);
            Play.gameObject.SetActive(true);
            return;
        }

        // Set buttons for the turn
        Hit.gameObject.SetActive(true);
        Stand.gameObject.SetActive(true);
        Double.gameObject.SetActive(true);
        Bet.gameObject.SetActive(false);       
        Chip1.gameObject.SetActive(false);
        Chip5.gameObject.SetActive(false);
        Chip10.gameObject.SetActive(false);
        Chip25.gameObject.SetActive(false);
        Chip50.gameObject.SetActive(false);
        Chip100.gameObject.SetActive(false);
        Chip500.gameObject.SetActive(false);
        Chip1000.gameObject.SetActive(false);
        Chip5000.gameObject.SetActive(false);
        Chip10000.gameObject.SetActive(false);
    }

    private void DealerTurn()
    {
        Hit.gameObject.SetActive(false);
        Stand.gameObject.SetActive(false);
        Double.gameObject.SetActive(false);
        Bet.gameObject.SetActive(false);
        // Continue drawing until dealer has at least 17
        while (dealerHandValue < 17)
        {
            string newCard = DrawRandomCard();
            AddCardOnScreen(dealerHandPositions[currentDealerIndex]);
            currentDealerIndex++;
            dealerHand.Add(newCard);
            DealerCardtext.text += " " + newCard;

            dealerHandValue = CalculateHandValue(dealerHand);
            DealerCardValuetext.text = "Value: " + dealerHandValue;
            Play.gameObject.SetActive(true);
        }

        if (dealerHandValue > 21 || playerHandValue > dealerHandValue)
        {
            GameResultText.text = "You win:" + BettingSystem.currentBet * 2;
            GameResultText.gameObject.SetActive(true);
            BettingSystem.WinBet();
            Play.gameObject.SetActive(true);
        }
        else if (playerHandValue == dealerHandValue)
        {
            GameResultText.text = "Draw. Returned: " + BettingSystem.currentBet;
            GameResultText.gameObject.SetActive(true);
            BettingSystem.DrawBet();
            Play.gameObject.SetActive(true);
        }
        else
        {
            GameResultText.text = "Dealer win";
            GameResultText.gameObject.SetActive(true);
            BettingSystem.LoseBet();
            Play.gameObject.SetActive(true);
        }
        Play.gameObject.SetActive(true);
    }
}
