using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;  
 

public class CardSelectionManager : MonoBehaviour
{
    public GameObject cardSelectionUI;
    public GameObject[] cardButtons;

    public EnemySpawner enemySpawner;
    public PlayerStats playerStats;
    public PlayerMovement playerMovement;
   


    private List<Card> availableCards = new List<Card>(); // Pool of cards
    private bool hasSelectedCards = false; // Flag to track if cards have been selected

    void Start()
    {
        cardSelectionUI.SetActive(false); // Initially hide the card selection UI
        LoadAvailableCards();  // Load all the available cards
    }

    private void LoadAvailableCards()
    {
        availableCards.AddRange(Resources.LoadAll<Card>("Cards"));
    }

    public void ShowCardSelection()
    {
        Debug.Log("Showing card selection UI.");
        cardSelectionUI.SetActive(true);  // Show the card selection UI
       
        playerMovement.StopAllMovement(); // Stops the current player movement
        playerMovement.enabled = false;  // Disable player movement while selecting cards

        // Make sure all buttons are active
        foreach (GameObject btn in cardButtons)
        {
            btn.SetActive(true);  // Enable each button 
        }

        // Select 3 random cards from the available pool
        List<Card> randomCards = GenerateRandomCards();

        // Assign cards to buttons
        for (int i = 0; i < cardButtons.Length; i++)
        {
            if (i < randomCards.Count)
            {
                Card card = randomCards[i];
                cardButtons[i].SetActive(true); // Ensure the button is active

                // Gather text components
                TextMeshProUGUI[] texts = cardButtons[i].GetComponentsInChildren<TextMeshProUGUI>();

                // update button with card information

                Image btnImage = cardButtons[i].GetComponentInChildren<Image>();
                if (texts[0].text != null)
                {
                    texts[0].text = card.name;
                    texts[1].text = card.description;
                }
                if (btnImage != null && card.cardImage != null)
                {
                    btnImage.sprite = card.cardImage; // Set the card's image
                }

                // Assign card effect to button
                int index = i; // Prevent closure issues
                cardButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
                cardButtons[i].GetComponent<Button>().onClick.AddListener(() => SelectCard(card));
            }
            else
            {
                cardButtons[i].SetActive(false); // Hide unused buttons
            }
        }
    }

    public void SelectCard(Card selectedCard)
    {
        // Apply the card's effect to the player
        playerStats.ApplyCardEffect(selectedCard.speedBonus, selectedCard.fireRateBonus, selectedCard.rangeBonus ,selectedCard.shotSpeedBonus);

        // Set the selected card flag to true
        hasSelectedCards = true;

        // Hide the UI after selecting a card
        cardSelectionUI.SetActive(false);

        // Enable player movement after card selection
        playerMovement.enabled = true;
       

        // Call the method to mark the card as selected and stop the popup UI
        enemySpawner.OnCardSelected();  // Trigger the enemy spawner to continue

        // Resume the wave spawner (if applicable)
        enemySpawner.StartSpawning();  // Start or resume spawning after selecting cards
    }


    // Check if cards have been selected
    public bool HasSelectedCards()
    {
        return hasSelectedCards;
    }

    // Selecr 3 random cards from the available cards pool
    private List<Card> GenerateRandomCards()
    {
        List<Card> randomCards = new List<Card>();
        for (int i = 0; i < 3; i++)
        {
            if (availableCards.Count > 0)
            {
                int randomIndex = Random.Range(0, availableCards.Count);
                randomCards.Add(availableCards[randomIndex]);
            }
        }
        return randomCards;
    }


}
