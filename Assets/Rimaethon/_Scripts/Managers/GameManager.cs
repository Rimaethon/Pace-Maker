using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Text score;
    public Text countdownText;
    private bool isCountdownMade;

    private int scoreCount;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
        StartCoroutine(FreezeGameWithCountdown());

    }

    private void Update()
    {
        score.text = "Score: " + scoreCount;
    }

    public void IncrementScore()
    {
        scoreCount++;
    }

    IEnumerator FreezeGameWithCountdown()
    {
        Time.timeScale = 0f; // Freezing the game

        countdownText.gameObject.SetActive(true);

        // Countdown from 5 to 1
        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }

        // Show "Start" text
        countdownText.text = "Start!";
        yield return new WaitForSecondsRealtime(1); // Keep it on screen for 1 second

        // Hide countdown text and resume game
        countdownText.gameObject.SetActive(false);
        Time.timeScale = 1f; // Resuming the game
    }
}