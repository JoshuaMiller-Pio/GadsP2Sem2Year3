using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement

    public bool isPlayer1 = true; // Toggle to check which player this script is for
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get input from Player 1 (WASD) or Player 2 (arrow keys)
        if (isPlayer1)
        {
            movement.x = Input.GetAxisRaw("Horizontal1"); // Player 1 (A/D for left/right)
            movement.y = Input.GetAxisRaw("Vertical1");   // Player 1 (W/S for up/down)
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal2"); // Player 2 (arrow keys left/right)
            movement.y = Input.GetAxisRaw("Vertical2");   // Player 2 (arrow keys up/down)
        }
    }

    void FixedUpdate()
    {
        // Move the player based on input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}