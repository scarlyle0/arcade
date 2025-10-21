using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBall();
    }

    void LaunchBall()
    {
        float zDirection = Random.value < 0.5f ? -1f : 1f;
        float xDirection = Random.Range(-0.5f, 0.5f);

        Vector3 direction = new Vector3(xDirection, 0, zDirection).normalized;
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Vector3 velocity = rb.linearVelocity;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Add some up/down variation
            velocity.x += Random.Range(-0.2f, 0.2f);
            speed *= 1.05f; // speed up slightly
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Add some horizontal nudge when bouncing vertically
            velocity.z += Random.Range(-0.2f, 0.2f);
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            // Reset ball position and speed
            transform.position = new Vector3(0, 0.5f, 0);
            speed = 8f;
            LaunchBall();
            return;
        }

        rb.linearVelocity = velocity.normalized * speed;
    }
}
