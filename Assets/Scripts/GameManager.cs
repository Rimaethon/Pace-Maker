using UnityEngine;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Text score;
    private int scoreCount;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }
    public void IncrementScore()
    {
        scoreCount++;
    }
    private void Update()
    {
        score.text = "Score:" + scoreCount;
    }
}
