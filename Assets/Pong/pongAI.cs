using UnityEngine;

public class AIPaddleController : MonoBehaviour
{
    public Transform ball;
    public float moveSpeed = 6f;
    public float reactionDelay = 0.2f;
    public float wallLimit = 3.5f;
    public float inaccuracy = 0.5f;

    private float targetX;

    void Update()
    {
        if (ball == null) return;

        // Predict where the ball will be slightly in the future
        float predictedX = ball.position.x + Random.Range(-inaccuracy, inaccuracy);

        // Slowly update target (simulates delayed reaction)
        targetX = Mathf.Lerp(targetX, predictedX, Time.deltaTime / reactionDelay);

        // Move paddle toward target position
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Clamp(
            Mathf.MoveTowards(newPos.x, targetX, moveSpeed * Time.deltaTime),
            -wallLimit, wallLimit
        );
        transform.position = newPos;
    }
}
