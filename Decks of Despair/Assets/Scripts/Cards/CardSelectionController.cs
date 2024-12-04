using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectionController : MonoBehaviour
{
    public Button[] cardButtons;       // Array of card buttons in the UI
    private int selectedIndex = 0;    // Tracks the currently selected card

    void Start()
    {
        HighlightCard(selectedIndex); // Highlight the first card at start
    }

    void Update()
    {
        if (!CardSelectionManager.isInputLocked) return; // Only allow selection during input lock

        // Navigate cards with left/right arrow keys
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Navigate(1); // Move to the next card
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Navigate(-1); // Move to the previous card
        }

        // Select the current card with spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SelectCard();
        }
    }

    private void Navigate(int direction)
    {
        // Unhighlight current card
        UnhighlightCard(selectedIndex);

        // Update selectedIndex with wrap-around
        selectedIndex = (selectedIndex + direction + cardButtons.Length) % cardButtons.Length;

        // Highlight the new card
        HighlightCard(selectedIndex);
    }

    private void HighlightCard(int index)
    {
        // Add visual indication for the selected card 
        cardButtons[index].GetComponent<Image>().color = Color.yellow;
    }

    private void UnhighlightCard(int index)
    {
        // Revert visual indication
        cardButtons[index].GetComponent<Image>().color = Color.white;
    }

    private void SelectCard()
    {
        // Trigger the card button’ss onClick event
        cardButtons[selectedIndex].onClick.Invoke();

    }
}
