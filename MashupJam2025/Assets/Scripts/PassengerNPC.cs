using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PassengerNPC : MonoBehaviour
{
    public PassengerData passengerData;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
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
}
