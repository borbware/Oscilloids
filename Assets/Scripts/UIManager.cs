using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] SimpleHelvetica scoreText;
    public static UIManager instance;
    [SerializeField] Image[] lifeImages;

    void Start()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        UpdateScore(GameManager.instance.score);
        UpdateLives(GameManager.instance.lives);
    }

    public void UpdateScore(int score)
    {
        scoreText.Text = score.ToString();
        for (int i = 0; i < 10; i++)
        {
            if (scoreText.Text.Length < 10)
                scoreText.Text = "-" + scoreText.Text;
        }
        scoreText.GenerateText();

    }
    public void UpdateLives(int lives)
    {
        for (int i = 1; i < lifeImages.Length + 1; i++)
        {
            lifeImages[i - 1].enabled = lives >= i;
        }
    }
}
