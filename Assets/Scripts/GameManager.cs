using UnityEngine;
using UnityEngine.SceneManagement;
using AudioRender;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lives = 4;
    public int score = 0;
    public float fadeTime = 0;
    float fadeStartVal = 0;
    float fadeEndVal = 0;
    float fadeLength = 0;
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
        if (fadeTime > 0)
        {
            WireframeRenderer.Instance.randomOffset = 
                Mathf.Lerp(fadeStartVal, fadeEndVal, (fadeLength - fadeTime) / fadeLength);
            fadeTime -= Time.deltaTime;
            if (fadeTime <= 0)
            {
                WireframeRenderer.Instance.randomOffset = 0f; 
                fadeTime = 0;
            }
        }
    }

    public void AddScore(int scoreChange)
    {
        score += scoreChange;
        if (UIManager.instance != null)
            UIManager.instance.UpdateScore(score);
    }
    public void Fade(float time, float startVal, float endVal)
    {
        fadeTime = time;
        fadeLength = time;
        fadeStartVal = startVal;
        fadeEndVal = endVal;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void AddLives(int livesChange)
    {
        lives += livesChange;
        if (UIManager.instance != null)
            UIManager.instance.UpdateLives(lives);
        if (lives == 0)
        {
            float time = 3f;
            Fade(time, 0f, 15f);
            LevelManager.instance.FadeOutMusic(time);
            Invoke("GameOver",time - 0.1f);
        }
    }
}
