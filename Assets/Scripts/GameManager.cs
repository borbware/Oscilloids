using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lives = 4;
    public int score = 0;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void AddScore(int scoreChange)
    {
        score += scoreChange;
        if (UIManager.instance != null)
            UIManager.instance.UpdateScore(score);
    }

    public void AddLives(int livesChange)
    {
        lives += livesChange;
        if (UIManager.instance != null)
            UIManager.instance.UpdateLives(lives);
    }
}
