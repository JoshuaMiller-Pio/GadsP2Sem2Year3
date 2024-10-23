using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Speed of movement
    public float rayDistance = 1.3f; // Raycast distance for interaction detection
    public LayerMask ingredientLayer; // LayerMask to detect ingredients
    public LayerMask stoveLayer;      // LayerMask for stoves
    public LayerMask choppingBoardLayer; // LayerMask for chopping boards

    public bool isPlayer1 = true;  // Toggle to check which player this script is for
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.right; // Default facing direction is right

    private GameObject heldObject = null; // Reference to the object being held
    public bool isHoldingObject = false;  // Bool to check if the player is holding an object
    private bool isCooking = false;  // Bool to check if the player is cooking

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        if (isCooking) return; // Prevent movement if player is cooking

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
            HandlePickupOrDrop();
        }
        else if (!isPlayer1 && Input.GetKeyDown(KeyCode.Keypad5))
        {
            HandlePickupOrDrop();
        }

        // Handle cooking and chopping actions
        if (isPlayer1 && Input.GetKeyDown(KeyCode.K))  // Player 1 presses 'K'
        {
            HandleCookingOrChopping();
        }
        else if (!isPlayer1 && Input.GetKeyDown(KeyCode.Keypad6))  // Player 2 presses 'Num6'
        {
            HandleCookingOrChopping();
        }
    }

    void FixedUpdate()
    {
        if (isCooking) return; // Prevent movement if player is cooking

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
            RaycastHit2D hit = Physics2D.Raycast(rb.position, lastDirection, rayDistance, ingredientLayer);
            if (hit.collider != null)  // If the raycast hits an ingredient
            {
                PickupObject(hit.collider.gameObject);
                if (heldObject.GetComponent<IngredientScript>() != null) 
                    heldObject.GetComponent<IngredientScript>().isheld = true;
            }
        }
    }

    void HandleCookingOrChopping()
    {
        if (heldObject != null)
        {
            RaycastHit2D hitStove = Physics2D.Raycast(rb.position, lastDirection, rayDistance, stoveLayer);
            RaycastHit2D hitchopper = Physics2D.Raycast(rb.position, lastDirection, rayDistance, choppingBoardLayer);
            IngredientScript heldScript = heldObject.GetComponent<IngredientScript>();
            if (hitchopper.collider != null && heldScript.canChop  && heldScript != null)  
            {
                StartCooking();  
            }
            if (hitStove.collider != null&& heldScript.canCook&& heldScript != null )  
            {
                StartCooking();  
            }
        }
    }

    // Method to start cooking and lock movement for 5 seconds
    public void StartCooking()
    {
        if (!isCooking) // Only start cooking if not already cooking
        {
            Debug.Log("Started Cooking");
            StartCoroutine(CookingCoroutine());
        }
    }

    // Coroutine to handle the 5-second cooking process
    IEnumerator CookingCoroutine()
    {
        isCooking = true;  // Lock movement

        // Simulate cooking by waiting for 5 seconds
        yield return new WaitForSeconds(5f);

        isCooking = false;  // Unlock movement after cooking is done

        IngredientScript heldScript = heldObject.GetComponent<IngredientScript>();
        IngredientsScriptable placeholder = new IngredientsScriptable();
        
        placeholder.ingredient = heldScript.ingredientScript.ingredient;
        placeholder.processedIngredient = heldScript.ingredientScript.processedIngredient;
        placeholder.ProcessedfoodSprite = heldScript.ingredientScript.ProcessedfoodSprite;
        placeholder.foodSprite = heldScript.ingredientScript.foodSprite;
        
        heldScript.ingredientScript = placeholder;
        heldScript.ingredientScript.ingredient = heldScript.ingredientScript.processedIngredient;
        heldObject.GetComponent<SpriteRenderer>().sprite = heldScript.ingredientScript.ProcessedfoodSprite;
        Debug.Log("Cooking Finished");
    }

    void PickupObject(GameObject obj)
    {
        isHoldingObject = true;
        heldObject = obj;

        heldObject.GetComponent<Collider2D>().enabled = false;

        if (heldObject.GetComponent<Rigidbody2D>() != null)
        {
            heldObject.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    void DropHeldObject()
    {
        isHoldingObject = false;
        if (heldObject.GetComponent<IngredientScript>() != null) 
            heldObject.GetComponent<IngredientScript>().isheld = false;

        heldObject.GetComponent<Collider2D>().enabled = true;

        Vector2 dropPosition = rb.position + lastDirection * rayDistance;
        heldObject.transform.position = dropPosition;

        if (heldObject.GetComponent<Rigidbody2D>() != null)
        {
            heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        heldObject = null;
    }
}
