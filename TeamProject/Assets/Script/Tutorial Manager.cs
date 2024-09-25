using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
public class TutorialManager : MonoBehaviour
{
    // Array to hold all tutorial pages (images)
    public GameObject[] tutorialPages;
    private int currentPage = 0;

    public GameObject tutorialPanel;
    public Flowchart fungusFlowchart; // Reference to the Fungus Flowchart
    public string cutsceneBlockName;  // The name of the Fungus Block to start the cutscene

    // Reference to the Next button (optional for manual progression)
    public Button nextButton;
    public Button backButton;  // Optional Back button for navigating backward

    private void Start()
    {
        // Initially hide all tutorial pages
        foreach (GameObject page in tutorialPages)
        {
            page.SetActive(false);
        }

        // Show the first tutorial page
        ShowCurrentPage();

        // Set up the Next and Back buttons if they are used
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextPage);
        }
        if (backButton != null)
        {
            backButton.onClick.AddListener(PreviousPage);
        }
    }

    // Show the current tutorial page
    private void ShowCurrentPage()
    {
        if (currentPage < tutorialPages.Length)
        {
            tutorialPages[currentPage].SetActive(true);
        }

        // Enable or disable buttons based on the current page
        if (nextButton != null)
        {
            nextButton.interactable = currentPage < tutorialPages.Length - 1;
        }
        if (backButton != null)
        {
            backButton.interactable = currentPage > 0;
        }
    }

    // Hide the current page and show the next one
    public void NextPage()
    {
        if (currentPage < tutorialPages.Length - 1)
        {
            tutorialPages[currentPage].SetActive(false);  // Hide current page
            currentPage++;  // Increment to the next page
            ShowCurrentPage();  // Show the next page
        }
    }

    // Show the previous tutorial page
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            tutorialPages[currentPage].SetActive(false);  // Hide current page
            currentPage--;  // Decrement to the previous page
            ShowCurrentPage();  // Show the previous page
        }
    }


    public void EndTutorial()
    {
        // Hide the tutorial panel
        tutorialPanel.SetActive(false);

        // Start the cutscene in Fungus
        fungusFlowchart.ExecuteBlock(cutsceneBlockName);
    }

}