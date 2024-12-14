using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement; // Reference to PlayerMovement script
    [SerializeField] private TextMeshProUGUI betText; // Text field for displaying the current bet amount

    public int currentBet = 0;

    private void Start()
    {
        UpdateUI();
    }

    // Method to place a bet
    public void PlaceBet(int amount)
    {
        if (amount <= PersistentData.Money) // Check if player has enough money
        {
            currentBet += amount;
            PersistentData.Money -= amount; // Deduct bet amount from player's money
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough money to place this bet.");
        }
    }

    // Method to handle standard win (2x payout)
    public void WinBet()
    {
        PersistentData.Money += currentBet * 2; // Player wins double their bet
        UpdateUI();
    }

    // Method to handle a Blackjack win (1.5x payout)
    public void BlackjackWin()
    {
        PersistentData.Money += (int)(currentBet * 2.5); // Player wins 1.5x their bet
        UpdateUI();
    }

    // Method for losing the bet
    public void LoseBet()
    {
        currentBet = 0; // Clear the current bet
    }

    // Method for drawing the bet (refund)
    public void DrawBet()
    {
        PersistentData.Money += currentBet; // Player gets their money back        
        UpdateUI();
    }

    // Optional: Double the current bet for a "Double" action
    public void DoubleBet()
    {
        Debug.Log("Double pressed");
        if (currentBet <= PersistentData.Money)
        {
            Debug.Log("player has enough money for double");
            PersistentData.Money -= currentBet; // Deduct the additional bet amount
            Debug.Log("money has been deducted");
            currentBet += currentBet;
            Debug.Log("money has been doubled to " +  currentBet);
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough money to double the bet.");
        }
    }

    // Update UI to display the bet and player money
    public void UpdateUI()
    {
        betText.text = "Bet: $" + currentBet;
        playerMovement.UpdateCoinText(); // Call the method to update player's money display
    }

    // Getter for current bet amount
    public int GetCurrentBet()
    {
        return currentBet;
    }

    public void ResetBet()
    {
        currentBet = 0;
    }
}
