using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    [SerializeField] Image[] lifeImages;
    [SerializeField] TextMeshProUGUI scoreText;


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
        // scoreText.text = score.ToString();
    }
    public void UpdateLives(int lives)
    {
        for (int i = 1; i < lifeImages.Length + 1; i++)
        {
            lifeImages[i - 1].enabled = lives >= i;
        }
    }
}
