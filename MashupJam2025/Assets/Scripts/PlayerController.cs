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
    private float verticalInput;


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

        if (isClimbing)
        {
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, verticalInput * climbSpeed);
        }
        else
        {
            rb.gravityScale = 1;
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
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}
