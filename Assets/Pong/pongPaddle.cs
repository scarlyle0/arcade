using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public float speed = 5f;
    private float moveInput;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
        Debug.Log("Move Input: " + moveInput);
    }

    void Update()
    {
        // Move along Z-axis
        Vector3 move = new Vector3(moveInput, 0, 0) * speed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }
}
