using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float joystickDeadZone = 0.2f;


    public FloatingJoystick joystick;

    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    private Vector2 movement;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        ReadJoystickInput();
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void ReadJoystickInput()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector2 input = new Vector2(horizontal, vertical);

        // Ignore very small joystick movement
        if (input.magnitude < joystickDeadZone)
        {
            movement = Vector2.zero;
            return;
        }

        // Lock to 4 directions based on stronger joystick direction
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            movement = new Vector2(Mathf.Sign(horizontal), 0f);
        }
        else
        {
            movement = new Vector2(0f, Mathf.Sign(vertical));
        }
    }

    private void UpdateAnimation()
    {
        if (movement != Vector2.zero)
        {
            playerAnimator.SetFloat("MoveX", movement.x);
            playerAnimator.SetFloat("MoveY", movement.y);
        }

        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);
    }
}