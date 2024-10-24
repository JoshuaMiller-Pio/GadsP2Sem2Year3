using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;   // Speed of movement
    public float rayDistance = 1.3f; // Raycast distance for interaction detection
    public LayerMask ingredientLayer; // LayerMask to detect ingredients
    public LayerMask stoveLayer;      // LayerMask for stoves
    public LayerMask choppingBoardLayer; // LayerMask for chopping boards

    public bool isPlayer1 = true;  // Boolean to check if it's Player 1 or Player 2

    // Sprites for the player's head in different directions
    public Sprite headDown;
    public Sprite headRight;
    public Sprite headUp;

    // Sprites for the player's body in different directions (on a different GameObject)
    public Sprite bodyDown;
    public Sprite bodyRight;
    public Sprite bodyUp;

    // Reference to the body GameObject's SpriteRenderer (assigned in the inspector)
    public SpriteRenderer bodySpriteRenderer;

    // Reference to the head SpriteRenderer (on the same GameObject as this script)
    private SpriteRenderer headSpriteRenderer;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastDirection = Vector2.down; // Default facing direction is down

    private GameObject heldObject = null; // Reference to the object being held
    public bool isHoldingObject = false;  // Bool to check if the player is holding an object
    private bool isCooking = false;  // Bool to check if the player is cooking

    // Reference to the timer GameObject (with the 3-second animation)
    public GameObject timerObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        headSpriteRenderer = GetComponent<SpriteRenderer>(); // Get the head's SpriteRenderer

        // Make sure the timer is initially disabled
        if (timerObject != null)
        {
            timerObject.SetActive(false);
        }
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
            movement.x = Input.GetAxisRaw("Horizontal2"); // Player 2 (Arrow keys left/right)
            movement.y = Input.GetAxisRaw("Vertical2");   // Player 2 (Arrow keys up/down)
        }

        // Update last direction based on movement
        if (movement.x > 0) lastDirection = Vector2.right;   // Facing right
        else if (movement.x < 0) lastDirection = Vector2.left;   // Facing left
        else if (movement.y > 0) lastDirection = Vector2.up;     // Facing up
        else if (movement.y < 0) lastDirection = Vector2.down;   // Facing down

        // Change head and body sprites based on movement direction
        UpdateSpritesBasedOnDirection();

        // Handle pickup and drop action
        if (isPlayer1 && Input.GetKeyDown(KeyCode.J))  // Player 1 uses 'J' for pickup/drop
        {
            HandlePickupOrDrop();
        }
        else if (!isPlayer1 && Input.GetKeyDown(KeyCode.Keypad5))  // Player 2 uses NumPad '5'
        {
            HandlePickupOrDrop();
        }

        // Handle cooking and chopping actions
        if (isPlayer1 && Input.GetKeyDown(KeyCode.K))  // Player 1 presses 'K' to cook/chop
        {
            HandleCookingOrChopping();
        }
        else if (!isPlayer1 && Input.GetKeyDown(KeyCode.Keypad6))  // Player 2 presses NumPad '6' to cook/chop
        {
            HandleCookingOrChopping();
        }
    }

    void FixedUpdate()
    {
        if (isCooking) return; // Prevent movement if player is cooking

        // Move the player based on input
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void UpdateSpritesBasedOnDirection()
    {
        // Update the head and body sprites based on the direction the player is facing
        if (lastDirection == Vector2.right)
        {
            headSpriteRenderer.sprite = headRight;
            bodySpriteRenderer.sprite = bodyRight;
            headSpriteRenderer.flipX = false;  // Ensure right-facing sprite is not flipped
            bodySpriteRenderer.flipX = false;
        }
        else if (lastDirection == Vector2.left)
        {
            headSpriteRenderer.sprite = headRight;  // Use the right sprite, but flip it
            bodySpriteRenderer.sprite = bodyRight;  // Use the right sprite, but flip it
            headSpriteRenderer.flipX = true;  // Flip to face left
            bodySpriteRenderer.flipX = true;  // Flip to face left
        }
        else if (lastDirection == Vector2.up)
        {
            headSpriteRenderer.sprite = headUp;
            bodySpriteRenderer.sprite = bodyUp;
            headSpriteRenderer.flipX = false;  // Ensure it's not flipped
            bodySpriteRenderer.flipX = false;
        }
        else if (lastDirection == Vector2.down)
        {
            headSpriteRenderer.sprite = headDown;
            bodySpriteRenderer.sprite = bodyDown;
            headSpriteRenderer.flipX = false;  // Ensure it's not flipped
            bodySpriteRenderer.flipX = false;
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
            RaycastHit2D hitChopper = Physics2D.Raycast(rb.position, lastDirection, rayDistance, choppingBoardLayer);
            IngredientScript heldScript = heldObject.GetComponent<IngredientScript>();

            if (hitChopper.collider != null && heldScript.canChop && heldScript != null)
            {
                StartCookingOrChopping();  // Start chopping
            }
            if (hitStove.collider != null && heldScript.canCook && heldScript != null)
            {
                StartCookingOrChopping();  // Start cooking
            }
        }
    }

    public void StartCookingOrChopping()
    {
        if (!isCooking) // Only start if not already cooking/chopping
        {
            Debug.Log("Started Cooking/Chopping");
            StartCoroutine(CookingOrChoppingCoroutine());
        }
    }

    // Coroutine to handle the 3-second cooking/chopping process
    IEnumerator CookingOrChoppingCoroutine()
    {
        isCooking = true;  // Lock movement

        // Enable the timer GameObject
        if (timerObject != null)
        {
            timerObject.SetActive(true);
        }

        // Simulate cooking/chopping by waiting for 3 seconds
        yield return new WaitForSeconds(3f);

        isCooking = false;  // Unlock movement after cooking/chopping is done

        // Disable the timer GameObject after 3 seconds
        if (timerObject != null)
        {
            timerObject.SetActive(false);
        }

        // Process the ingredient and update it to the cooked/chopped state
        IngredientScript heldScript = heldObject.GetComponent<IngredientScript>();
        IngredientsScriptable placeholder = new IngredientsScriptable();

        placeholder.ingredient = heldScript.ingredientScript.ingredient;
        placeholder.processedIngredient = heldScript.ingredientScript.processedIngredient;
        placeholder.ProcessedfoodSprite = heldScript.ingredientScript.ProcessedfoodSprite;
        placeholder.foodSprite = heldScript.ingredientScript.foodSprite;

        heldScript.ingredientScript = placeholder;
        heldScript.ingredientScript.ingredient = heldScript.ingredientScript.processedIngredient;
        heldObject.GetComponent<SpriteRenderer>().sprite = heldScript.ingredientScript.ProcessedfoodSprite;

        Debug.Log("Cooking/Chopping Finished");
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
