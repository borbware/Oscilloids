using UnityEngine;

public class ShootTheBullet : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] AudioSource bulletAudio;
    [SerializeField] float bulletLifeTime;
    [SerializeField] Transform shotPosition;
    [SerializeField] float recoilForce;
    [SerializeField] float shotForce;
    [SerializeField] float shotPeriod;

    Rigidbody2D rb;

    float shotTime = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void CreateBullet(float rotation)
    {
        Quaternion newRotation = Quaternion.Euler(0, 0, rotation);

        GameObject newBullet = Instantiate(
            bullet,
            shotPosition.position,
            newRotation * transform.rotation  // rotate transform by newRotation
        );
        
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();

        // force direction is rotated by newRotation as well
        bulletRb.AddForce(
            newRotation * transform.up * shotForce
        );
        Destroy(newBullet, bulletLifeTime);
    }
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && shotTime <= 0)
        {
            CreateBullet(0);
            bulletAudio.Play();
            rb.AddForce(-transform.up * recoilForce);
            shotTime = shotPeriod;
        }
        if (Input.GetButton("Fire2") && shotTime <= 0)
        {
            for (int i = -3; i <= 3; i++)
            {
                CreateBullet(i * 10); // create rotations in increments of 10 degrees
            }
            bulletAudio.Play();
            rb.AddForce(-transform.up * recoilForce * 10);
            shotTime = shotPeriod * 10;
        }
        shotTime -= Time.deltaTime;
    }
}
