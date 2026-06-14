using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float speed = 5.0f; // Speed of the player movement
    
    private Rigidbody2D playerRb; // Reference to the Rigidbody2D component
    private Animator playerAnimator; // Reference to the Animator component

    private Vector2 movement; // Variable to store player movement input

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           playerRb = GetComponent<Rigidbody2D>();
           playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Get horizontal input
        movement.y = Input.GetAxisRaw("Vertical"); // Get vertical input
    
        playerAnimator.SetFloat("Move X", movement.x);
        playerAnimator.SetFloat("Move Y", movement.y);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude); // Set the speed parameter based on the magnitude of movement

    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime); // Move the player based on input and speed
    }
}