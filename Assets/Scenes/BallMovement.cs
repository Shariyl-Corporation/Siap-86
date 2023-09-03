using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get user input for horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the movement vector
        Vector2 movement = new Vector2(horizontalInput, 0f);

        // Apply force to the ball
        rb.AddForce(movement * moveSpeed);
    }
}
