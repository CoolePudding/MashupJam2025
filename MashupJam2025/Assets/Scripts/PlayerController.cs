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
    }

    void FixedUpdate()
    {
        var dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager != null && dialogueManager.dialogueIsPlaying)
            return;

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
}
