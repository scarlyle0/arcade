using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;
    public float wallLimit = 3.5f;
    private float moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }

    void Update()
    {
        Vector3 move = new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
        transform.position += move;

        // Keep paddle inside bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -wallLimit, wallLimit);
        transform.position = pos;
    }
}
