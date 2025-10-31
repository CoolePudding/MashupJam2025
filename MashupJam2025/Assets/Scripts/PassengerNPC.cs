using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PassengerNPC : MonoBehaviour
{
    public PassengerData passengerData;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Initialize(passengerData);
    }

    public void Initialize(PassengerData data)
    {
        passengerData = data;
        ApplyPortrait();
        ApplyAnimator();
    }

    public void ApplyPortrait()
    {
        if (passengerData != null && passengerData.portrait != null)
        {
            spriteRenderer.sprite = passengerData.portrait;
            spriteRenderer.enabled = true;
        }
        else
        {
            Debug.LogWarning($"{name}: Missing portrait sprite!");
        }
    }

    public void ApplyAnimator()
    {
        if (passengerData != null && passengerData.animatorController != null)
        {
            animator.runtimeAnimatorController = passengerData.animatorController;
        }
        else
        {
            Debug.LogWarning($"{name}: Missing animator controller!");
        }
    }

}
