using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] AudioSource explosionAudio;
    public float halfScreenWidth = 11.2f;
    public float halfScreenHeight = 6.3f;
    public List<GameObject> asteroids = new List<GameObject>();
    [SerializeField] int asteroidsToSpawn = 3;
    [SerializeField] GameObject Asteroid;
    
    [SerializeField] AudioMixerSnapshot volumeOn;
    [SerializeField] AudioMixerSnapshot volumeOff;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    void Start()
    {
        volumeOn.TransitionTo(0f);
        StartPhase();
    }
    public void FadeOutMusic(float time)
    {
        volumeOff.TransitionTo(time);
    }

    public void SpawnInvoke(float time)
    {
        Invoke("StartPhase",time);
    }    
    public void CheckAsteroidsInvoke()
    {
        CancelInvoke();
        Invoke("CheckAsteroids",2f);
    }
    public void CheckAsteroids()
    {
        if (asteroids.Count == 0)
        {
            float time = 1f;
            GameManager.instance.Fade(time,0f,15f);
            SpawnInvoke(time - 0.1f);
        }
    }
    void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidsToSpawn; i++)
        {
            Vector2 position;
            int tries = 10;
            do
            {
                position = Random.insideUnitCircle * halfScreenWidth;
                tries--;
            }
            while (PositionHasOther(position, 5) && tries > 0);

            Instantiate(Asteroid, position, Quaternion.identity);
        }
    }
    public void StartPhase()
    {
        asteroidsToSpawn++;
        SpawnAsteroids();
        GameManager.instance.Fade(1f,15f,0f);
    }
    public void PlayExplosion()
    {
        explosionAudio.Play();
    }
    public void RestartPhase()
    {
        for (int i = asteroids.Count - 1; i >= 0; i-- )
        {
            Destroy(asteroids[i]);
            asteroids.Remove(asteroids[i]);
        }
        SpawnAsteroids();
    }
    public bool PositionHasOther(Vector2 pos, float radius)
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        Collider2D[] results = new Collider2D[5];
        int collisions = Physics2D.OverlapCircle(pos, radius, contactFilter, results);
        return collisions > 0;
    }
    bool PositionHasOther3D(Vector3 pos, float radius) // for reference
    {
        return Physics.CheckSphere(pos, radius);
    }
}
