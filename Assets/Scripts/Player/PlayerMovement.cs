using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Speed of movement
    public float rayDistance = 1.3f; // Raycast distance for pickup detection
    public LayerMask ingredientLayer; // LayerMask to detect ingredients

    public bool isPlayer1 = true;  // Toggle to check which player this script is for
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.right; // Default facing direction is right

    private GameObject heldObject = null; // Reference to the object being held
    public bool isHoldingObject = false;  // Bool to check if the player is holding an object

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        // Get input for movement (Player 1 uses WASD, Player 2 uses arrow keys)
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

        // Update the last direction the player was moving towards
        if (movement.x > 0) lastDirection = Vector2.right;   // Facing right
        else if (movement.x < 0) lastDirection = Vector2.left;   // Facing left
        else if (movement.y > 0) lastDirection = Vector2.up;     // Facing up
        else if (movement.y < 0) lastDirection = Vector2.down;   // Facing down

        // Handle pickup and drop action
        if (isPlayer1 && Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("pressed j");
            HandlePickupOrDrop();
        }
        else if (!isPlayer1 && Input.GetKeyDown(KeyCode.Keypad5))
        {
            HandlePickupOrDrop();
        }
    }

    void FixedUpdate()
    {
        // Move the player based on input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Perform the raycast in the last direction the player faced
        RaycastHit2D hit = Physics2D.Raycast(rb.position, lastDirection, rayDistance, ingredientLayer);

        // Debugging: Draw the ray in the scene view (for visualization)
        Debug.DrawRay(rb.position, lastDirection * rayDistance, Color.red);

        // If the player is holding something, keep it above their head
        if (isHoldingObject && heldObject != null)
        {
            Vector2 holdPosition = rb.position + Vector2.up * 1.2f; // Held object stays above the player's head
            heldObject.transform.position = holdPosition;
        }
    }

    void HandlePickupOrDrop()
    {
        
        if (isHoldingObject)  // If the player is holding something, drop it
        {
            DropHeldObject();
        }
        else  // Otherwise, try to pick something up
        {
            Debug.Log("Cast");

            RaycastHit2D hit = Physics2D.Raycast(rb.position, lastDirection, rayDistance, ingredientLayer);
            if (hit.collider != null)  // If the raycast hits an ingredient
            {
                Debug.Log("HIT");
                PickupObject(hit.collider.gameObject);
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        isHoldingObject = true;
        heldObject = obj;

        // Disable the ingredient's collider so it doesn't interfere with movement
        heldObject.GetComponent<Collider2D>().enabled = false;

        // Optionally disable gravity or other physics interactions
        if (heldObject.GetComponent<Rigidbody2D>() != null)
        {
            heldObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void DropHeldObject()
    {
        isHoldingObject = false;

        // Enable the object's collider again
        heldObject.GetComponent<Collider2D>().enabled = true;

        // Place the object at the end of the raycast (in front of the player)
        Vector2 dropPosition = rb.position + lastDirection * rayDistance;
        heldObject.transform.position = dropPosition;

        // Re-enable physics interactions
        if (heldObject.GetComponent<Rigidbody2D>() != null)
        {
            heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        // Clear the reference to the held object
        heldObject = null;
    }
}
