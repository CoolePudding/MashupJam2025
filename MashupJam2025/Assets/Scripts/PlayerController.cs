using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float moveInput = 0;
        if (Keyboard.current.dKey.isPressed) { moveInput += 1; }
        if (Keyboard.current.aKey.isPressed) { moveInput += -1; }
        movement = new Vector2(moveInput, 0);

        if (moveInput != 0)
            sr.flipX = moveInput < 0;
    }

    void FixedUpdate()
    {
        var dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager != null && dialogueManager.dialogueIsPlaying)
            return;

        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }
}
