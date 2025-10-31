using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;
    private float lastMoveDirection = 1f;
    private Animator animator;

    public float climbSpeed = 3f;
    private bool isClimbing = false;
    private bool canClimb = false;
    private float verticalInput;
    public float gravity = 5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        float moveInput = 0;
        if (Keyboard.current.dKey.isPressed) { moveInput += 1; }
        if (Keyboard.current.aKey.isPressed) { moveInput += -1; }
        movement = new Vector2(moveInput, 0);

        if (moveInput != 0)
        {
            lastMoveDirection = moveInput;
            animator.SetBool("isWalking", true);
            sr.flipX = moveInput < 0;
        }

        else
        {
            sr.flipX = lastMoveDirection < 0;
            animator.SetBool("isWalking", false);
        }

        verticalInput = Input.GetAxis("Vertical");

        if (canClimb && !isClimbing)
        {
            if (Mathf.Abs(verticalInput) > 0.01f)
            {
                isClimbing = true;
            }
        }

        if (isClimbing)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalInput * climbSpeed);

            bool isActuallyClimbing = Mathf.Abs(verticalInput) > 0.01f;
            animator.SetBool("isClimbing", true);
            animator.SetFloat("verticalSpeed", isActuallyClimbing ? Mathf.Abs(verticalInput) : 0f);
            animator.speed = Mathf.Abs(verticalInput) > 0.01f ? 1 : 0;
        }
        else
        {
            rb.gravityScale = gravity;
            animator.SetBool("isClimbing", false);
            animator.SetFloat("verticalSpeed", 0f);
            animator.speed = 1;
        }

    }

    void FixedUpdate()
    {
        var dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager != null && dialogueManager.dialogueIsPlaying)
            return;

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = false;
            isClimbing = false;
        }
    }

}
