using UnityEngine;
using System.Collections.Generic;

public class PassengerManager : MonoBehaviour
{
    [Header("Passenger Pool")]
    public PassengerData[] allPassengers; 
    public GameObject passengerPrefab; 

    [Header("Spawn Settings")]
    public Transform[] spawnPoints; 

    private List<PassengerData> selectedPassengers = new List<PassengerData>();

    void Start()
    {
        SelectRandomPassengers(5);
        SpawnPassengers();
    }

    void SelectRandomPassengers(int count)
    {
        List<PassengerData> available = new List<PassengerData>(allPassengers);

        for (int i = 0; i < count && available.Count > 0; i++)
        {
            int index = Random.Range(0, available.Count);
            PassengerData chosen = available[index];
            selectedPassengers.Add(chosen);
            available.RemoveAt(index);
        }
    }

    void SpawnPassengers()
    {
        for (int i = 0; i < selectedPassengers.Count; i++)
        {
            Transform spawnPoint = spawnPoints[i % spawnPoints.Length];
            GameObject npc = Instantiate(passengerPrefab, spawnPoint.position, Quaternion.identity);
            npc.SetActive(true);

            PassengerNPC npcScript = npc.GetComponent<PassengerNPC>();
            npcScript.passengerData = selectedPassengers[i];
            npcScript.ApplyPortrait(); 

            var sr = npc.GetComponent<SpriteRenderer>();
        }
    }
}
