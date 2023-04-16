using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] AudioSource explosionAudio;
    [SerializeField] float maxAngularVelocity = 90;
    [SerializeField] float maxAngularVelocityWhenShooting = 90;
    [SerializeField] float maxVelocity = 100;
    [SerializeField] float thrust = 90;
    [SerializeField] float torque = 90;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump")) { // continuous
            if (rb.velocity.magnitude < maxVelocity)
                rb.AddForce(transform.up * thrust * Time.fixedDeltaTime);
            else
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
            // particles.Play();
        }
        else
        {
            // particles.Stop();
        }

        float rotateInput = Input.GetAxisRaw("Horizontal");
        
        float appliedMaxAngularVelocity = 
            (Input.GetButton("Fire1") || Input.GetButton("Fire2"))
                ? maxAngularVelocityWhenShooting
                : maxAngularVelocity; 
        if ( Mathf.Abs(rb.angularVelocity) <= appliedMaxAngularVelocity)
            rb.AddTorque(- rotateInput * Time.fixedDeltaTime * torque);
        else
            rb.angularVelocity = Mathf.Clamp(
                rb.angularVelocity,
                -appliedMaxAngularVelocity,
                appliedMaxAngularVelocity
            );
    }
    void Respawn()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameManager.instance.Fade(1f,15f,0f);
        LevelManager.instance.RestartPhase();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid" && GameManager.instance.fadeTime == 0)
        {
            GameManager.instance.AddLives(-1);
            explosionAudio.Play();
            if (GameManager.instance.lives > 0)
            {
                float time = 1f;
                GameManager.instance.Fade(time,0f,15f);
                Invoke("Respawn",time-0.1f);
            }

        }
    }    
}
