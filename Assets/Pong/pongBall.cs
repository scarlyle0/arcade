using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 8f;
    public float maxBounceAngle = 80f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        LaunchBall();
    }

    void LaunchBall()
    {
        float zDirection = 1f; //Random.value < 0.5f ? -1f : 1f;
        float xDirection = 0; // Random.Range(-0.5f, 0.5f);

        Vector3 direction = new Vector3(xDirection, 0, zDirection).normalized;
        rb.linearVelocity = direction * speed;
    }

    Vector3 GetHitDirection(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];

        // Convert contact to paddle local space
        Vector3 localHitPoint = collision.transform.InverseTransformPoint(contact.point);

        // Get half the paddle's height regardless of scale (using local x-axis)
        float halfHeight = collision.collider.bounds.extents.x / collision.transform.lossyScale.x;

        // from [-halfHeight, halfHeight] to [0, 1]
        float normalizedX = Mathf.InverseLerp(-halfHeight, halfHeight, localHitPoint.x);

        // Change from [0,1] to [-0.5, 0.5]
        float hitOffset = normalizedX - 0.5f;

        // Calculate a bounce angle between -maxBounceAngle and +maxBounceAngle
        float bounceAngle = hitOffset * 2f * maxBounceAngle;

        // Determine which side the ball came from (left/right)
        float paddleDir = Mathf.Sign(rb.linearVelocity.z); // negative if coming from right, positive if from left

        // Base direction: reflect horizontally
        Vector3 baseDir = new Vector3(0, 0, paddleDir);

        // Rotate around Y to add vertical (X) angle
        Vector3 newDir = Quaternion.Euler(0, -bounceAngle, 0) * baseDir;

        Debug.Log($"Bounce angle: {bounceAngle}, Resulting dir: {newDir}");

        return newDir.normalized;
    }



    void OnCollisionEnter(Collision collision)
    {

        Vector3 velocity = rb.linearVelocity;

        if (collision.gameObject.CompareTag("Paddle"))
        {
            Vector3 bounceDirection = GetHitDirection(collision);
            speed *= 1.05f;
            rb.linearVelocity = bounceDirection.normalized * speed;
            return;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // Add some horizontal nudge when bouncing vertically
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            // Reset ball position and speed
            transform.position = new Vector3(0, 0.5f, 0);
            speed = 8f;
            LaunchBall();
            return;
        }
    }

}
