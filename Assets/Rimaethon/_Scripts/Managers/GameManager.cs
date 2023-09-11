using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon._Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public Text score;
        public Text countdownText;
        private bool isCountdownMade;

        private int scoreCount;

    

    
        private void Awake()
        {
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
            Time.timeScale = 0f; 

            countdownText.gameObject.SetActive(true);

            for (int i = 5; i > 0; i--)
            {
                countdownText.text = i.ToString();
                yield return new WaitForSecondsRealtime(1);
            }

            countdownText.text = "Start!";
            yield return new WaitForSecondsRealtime(1); 

            countdownText.gameObject.SetActive(false);
            Time.timeScale = 1f; 
        }
    }
}