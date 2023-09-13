using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject settingsPanel;
    public GameObject mainMenuButtons;
    public GameObject magicOrb;
    private ConstrainedMouseMovement _constrainedMouseMovementScript;


    private void Start()
    {
        // Get references to the buttons and add click listeners

        _constrainedMouseMovementScript = magicOrb.GetComponent<ConstrainedMouseMovement>();
        // Hide the pause menu at the start of the game
        pauseMenuPanel.SetActive(false);
        //settingsPanel.SetActive(false);
    }

    private void Update()
    {
        // Check for the Escape key to be pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the pause menu is not active, show it and pause the game
            if (!pauseMenuPanel.activeSelf)
            {
                pauseMenuPanel.SetActive(true);
                mainMenuButtons.SetActive(true);
                _constrainedMouseMovementScript.enabled = false;


                Time.timeScale = 0;
            }
            // If the pause menu is active, hide it and resume the game
            else
            {
                CloseMenu();
                Time.timeScale = 1;
            }
        }
    }

    public void ResumeGame()
    {
        CloseMenu();
        Time.timeScale = 1;
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseMenu()
    {
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        _constrainedMouseMovementScript.enabled = true;
    }
}