using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float halfScreenWidth = 11.2f;
    public float halfScreenHeight = 6.3f;
    public List<GameObject> asteroids = new List<GameObject>();
    [SerializeField] int asteroidsToSpawn = 3;
    [SerializeField] GameObject Asteroid;
    
    [SerializeField] AudioMixerSnapshot volumeOn;
    [SerializeField] AudioMixerSnapshot volumeOff;
    void Start()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        SpawnAsteroids();
    }

    public void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidsToSpawn; i++)
        {
            Vector2 position;
            int tries = 10;
            do
            {
                position = new Vector2(
                    Random.Range(-halfScreenWidth, halfScreenWidth),
                    Random.Range(-halfScreenHeight, halfScreenHeight)
                );
                tries--;
            }
            while (PositionHasOther(position, 5) && tries > 0);

            Instantiate(Asteroid, position, Quaternion.identity);
        }
        asteroidsToSpawn++;
    }
    bool PositionHasOther(Vector2 pos, float radius)
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
