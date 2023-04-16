using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] Transform pos3;
    [SerializeField] GameObject tinyAsteroid;
    [SerializeField] GameObject Explosion;

    [SerializeField] int hp;
    [SerializeField] float hurtPeriod;
    [SerializeField] float bulletPushForce;
    [SerializeField] float initTorque;
    [SerializeField] float initForce;
    [SerializeField] int hurtPoints = 10;
    [SerializeField] int killPoints = 1000;
    float hurtTime = 1f;
    Renderer rend;
    Rigidbody2D rb;

    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        rb.AddTorque(Random.Range(-1f, 1f) * initTorque);
        rb.AddForce(Random.insideUnitCircle * initForce);

        LevelManager.instance.asteroids.Add(gameObject);
    }

    void Update()
    {
        HurtUpdate();
    }
    void UpdateLevelManager()
    {
        LevelManager.instance.asteroids.Remove(gameObject);
        LevelManager.instance.CheckAsteroidsInvoke();
    }
    void SpawnTinyAsteroids()
    {
        if (pos1 != null)
            Instantiate(tinyAsteroid, pos1.position, transform.rotation);
        if (pos2 != null)
            Instantiate(tinyAsteroid, pos2.position, transform.rotation);
        if (pos3 != null)
            Instantiate(tinyAsteroid, pos3.position, transform.rotation);
    }
    void KillSelf()
    {
        GameManager.instance.AddScore(killPoints);
        SpawnTinyAsteroids();
        UpdateLevelManager();
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
        LevelManager.instance.PlayExplosion();
    }
    void HurtSelf(GameObject hurter)
    {
        GameManager.instance.AddScore(hurtPoints);
        hurtTime = 0f;
        rb.AddForce(hurter.transform.up * bulletPushForce);
    }
    void HurtUpdate()
    {
        if (hurtTime < hurtPeriod)
        {

        }
        hurtTime += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            hp -= 1;
            if (hp == 0)
                KillSelf();
            else
                HurtSelf(other.gameObject);
        }
    }
}
