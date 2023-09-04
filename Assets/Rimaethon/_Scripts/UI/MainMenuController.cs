using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void GoToGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}