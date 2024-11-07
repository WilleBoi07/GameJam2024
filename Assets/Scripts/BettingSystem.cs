using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BettingSystem : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement; // Reference to PlayerMovement script
    [SerializeField] private TextMeshProUGUI betText; // Text field for displaying the current bet amount

    private int currentBet = 0;

    private void Start()
    {
        UpdateUI();
    }

    // Method to place a bet
    public void PlaceBet(int amount)
    {
        if (amount <= playerMovement.Money) // Check if player has enough money
        {
            currentBet += amount;
            playerMovement.Money -= amount; // Deduct bet amount from player's money
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
        playerMovement.Money += currentBet * 2; // Player wins double their bet
        currentBet = 0;
        UpdateUI();
    }

    // Method to handle a Blackjack win (1.5x payout)
    public void BlackjackWin()
    {
        playerMovement.Money += (int)(currentBet * 2.5); // Player wins 1.5x their bet
        currentBet = 0;
        UpdateUI();
    }

    // Method for losing the bet
    public void LoseBet()
    {
        currentBet = 0; // Clear the current bet
        UpdateUI();
    }

    // Method for drawing the bet (refund)
    public void DrawBet()
    {
        playerMovement.Money += currentBet; // Player gets their money back
        currentBet = 0;
        UpdateUI();
    }

    // Optional: Double the current bet for a "Double" action
    public void DoubleBet()
    {
        if (currentBet <= playerMovement.Money)
        {
            playerMovement.Money -= currentBet; // Deduct the additional bet amount
            currentBet *= 2;
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough money to double the bet.");
        }
    }

    // Update UI to display the bet and player money
    private void UpdateUI()
    {
        betText.text = "Bet: $" + currentBet;
        playerMovement.UpdateCoinText(); // Call the method to update player's money display
    }

    // Getter for current bet amount
    public int GetCurrentBet()
    {
        return currentBet;
    }
}
