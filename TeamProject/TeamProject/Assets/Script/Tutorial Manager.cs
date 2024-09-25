using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Array to hold all tutorial pages (images)
    public GameObject[] tutorialPages;
    private int currentPage = 0;

    // Reference to the Next button (optional for manual progression)
    public Button nextButton;
    public Button backButton;  // Optional Back button for navigating backward
    public GameObject[] TickButton;
    public AudioSource audioSource;
    public AudioClip SpacebarSound;

    private void Start()
    {
        // Initially hide all tutorial pages
        foreach (GameObject page in tutorialPages)
        {
            page.SetActive(false);
        }

        foreach(GameObject Tick in TickButton)
        {
            Tick.SetActive(false);
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

    private void Update()
    {
        // Check if the spacebar is pressed and the current page is the last one
        if (currentPage == tutorialPages.Length - 1 && Input.GetKeyDown(KeyCode.Space))
        {
            PlaySpacebarSound();
            NextScene();  // Call the function to load the next scene
        }
        TickButtonManager();
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
            // Hide current page
            tutorialPages[currentPage].SetActive(false);
            // Increment to the next page
            currentPage++;
            // Show the next page
            ShowCurrentPage();
        }
    }

    // Show the previous tutorial page
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            // Hide current page
            tutorialPages[currentPage].SetActive(false);
            // Decrement to the previous page
            currentPage--;
            // Show the previous page
            ShowCurrentPage();
        }
    }

    // Method to load the next scene
    public void NextScene()
    {
        SceneManager.LoadScene("Game1");  // Replace "Game1" with the name or index of your scene
    }

    public void PlaySpacebarSound()
    {
        if (SpacebarSound!=null)
        {
            audioSource.PlayOneShot(SpacebarSound);

        }

    }

    public void TickButtonManager()
    {
        if (currentPage >= 2)
        {
            foreach (GameObject Tick in TickButton)
            {
                Tick.SetActive(true);

            }
        }
    }
}
