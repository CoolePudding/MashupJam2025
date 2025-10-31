using UnityEngine;
using System.Collections.Generic;

public class PassengerManager : MonoBehaviour
{
    public static PassengerManager Instance { get; private set; }

    [SerializeField] private GameObject passengerPrefab;
    [SerializeField] private Transform passengerParent;
    [SerializeField] private List<PassengerData> allPassengers;

    public readonly List<PassengerNPC> activePassengers = new();

    [Header("Initial Passengers")]
    public PassengerData[] initialPassengers;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;



    private void Awake()
    {
        Instance = this;
    }

    public void SpawnInitialPassengers()
    {
        for (int i = 0; i < initialPassengers.Length; i++)
        {
            Transform spawn = spawnPoints[i % spawnPoints.Length]; // fallback if fewer points than passengers
            SpawnPassenger(initialPassengers[i], spawn.position);
        }
    }


    public void SpawnPassenger(PassengerData data, Vector3 position = default)
    {
        if (position == default)
            position = passengerParent.position;

        GameObject passengerObj = Instantiate(passengerPrefab, position, Quaternion.identity, passengerParent);
        PassengerNPC npc = passengerObj.GetComponent<PassengerNPC>();
        npc.Initialize(data);
        activePassengers.Add(npc);
    }



    public void RemovePassenger(PassengerData data)
    {
        PassengerNPC npc = activePassengers.Find(p => p.passengerData == data);
        if (npc != null)
        {
            activePassengers.Remove(npc);
            Destroy(npc.gameObject);
        }
    }

    public PassengerData GetPassengerByName(string name)
    {
        // Normalize input
        string normalizedInput = name.Trim().ToLower();

        foreach (PassengerData data in allPassengers)
        {
            if (data == null || string.IsNullOrEmpty(data.passengerName))
                continue;

            // Split the full name to get the first name
            string[] parts = data.passengerName.Split(' ');
            string firstName = parts[0].ToLower();

            // Match either full name or just first name
            if (data.passengerName.ToLower() == normalizedInput || firstName == normalizedInput)
            {
                return data;
            }
        }

        Debug.LogWarning($"Passenger with name '{name}' not found in PassengerManager!");
        return null;
    }

}
