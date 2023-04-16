using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    void Start()
    {
        GameManager.instance.Fade(3f,15f,0f);
    }
    void Restart()
    {
        GameManager.instance.lives = 4;
        GameManager.instance.score = 0;
        SceneManager.LoadScene("Asteroids");
    }
    void Update()
    {
        if (Time.timeSinceLevelLoad > 2f && Input.anyKey && GameManager.instance.fadeTime == 0)
        {
            GameManager.instance.Fade(2f,0f,15f);
            Invoke("Restart",2f);
        }
    }
}
