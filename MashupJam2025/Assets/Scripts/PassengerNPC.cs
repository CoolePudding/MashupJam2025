using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PassengerNPC : MonoBehaviour
{
    public PassengerData passengerData;
    private SpriteRenderer spriteRenderer;

    private int currentHP;
    public bool IsDead => currentHP <= 0;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Initialize(passengerData);
    }

    public void Initialize(PassengerData data)
    {
        passengerData = data;
        currentHP = data.healthPoints;
        ApplyPortrait();
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

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Debug.Log($"{passengerData.passengerName} has died.");
        }
    }
}
