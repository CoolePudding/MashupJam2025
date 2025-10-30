using UnityEngine;

[CreateAssetMenu(fileName = "NewPassenger", menuName = "Passengers/Passenger Data")]
public class PassengerData : ScriptableObject
{
    [Header("Basic Info")]
    public string passengerName;
    public Sprite portrait;

    [Header("Dialogue per Phase")]
    public TextAsset[] dialoguePerPhase;

}