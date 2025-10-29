using UnityEngine;

[CreateAssetMenu(fileName = "NewPassenger", menuName = "Passengers/Passenger Data")]
public class PassengerData : ScriptableObject
{
    [Header("Basic Info")]
    public string passengerName;
    public Sprite portrait;
    public int healthPoints = 3;

    [TextArea(2, 4)]
    public string charDescription;

    [Header("Bonus")]
    public string[] bonus;

   

    [Header("Dialogue")]
    public TextAsset inkJSON; 
}