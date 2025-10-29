using UnityEngine;
using System.Collections.Generic;

public class PassengerManager : MonoBehaviour
{
    [Header("Scene Setup")]
    public Transform passengerParent;
    public GameObject passengerPrefab;
    public Transform[] spawnPoints; // <-- add this line

    [Header("Data Pool")]
    public List<PassengerData> possiblePassengers = new List<PassengerData>();

    public List<GameObject> activePassengers = new List<GameObject>();

    public void SpawnInitialPassengers(int count)
    {
        foreach (var go in activePassengers)
            if (go != null) Destroy(go);

        activePassengers.Clear();

        for (int i = 0; i < count; i++)
            SpawnRandomPassenger(i);
    }

    public void SpawnRandomPassenger(int index)
    {
        if (possiblePassengers == null || possiblePassengers.Count == 0)
        {
            Debug.LogWarning("No possiblePassengers left to spawn — all have been used!");
            return;
        }

        // Pick a random PassengerData
        int randomIndex = Random.Range(0, possiblePassengers.Count);
        PassengerData data = possiblePassengers[randomIndex];

        // Remove from pool so it cant be picked again
        possiblePassengers.RemoveAt(randomIndex);

        // Instantiate prefab
        GameObject passengerObj = Instantiate(passengerPrefab, passengerParent);

        // Assign spawn point if available
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Transform spawn = spawnPoints[index % spawnPoints.Length];
            passengerObj.transform.position = spawn.position;
        }
        else
        {
            // fallback: random small offset so they don’t overlap
            passengerObj.transform.localPosition = new Vector3(Random.Range(-2f, 2f), 0, 0);
        }

        // Apply data
        PassengerNPC passenger = passengerObj.GetComponent<PassengerNPC>();
        if (passenger == null)
        {
            Debug.LogError("Passenger prefab does not contain a PassengerNPC component!");
            Destroy(passengerObj);
            return;
        }

        passenger.Initialize(data);
        activePassengers.Add(passengerObj);
    }


    // Call when arriving at a station to consume food and apply consequences
    public void OnArriveAtStation()
    {
        var rm = GameManager.Instance.resourceManager;

        // iterate over a copy because we may remove during iteration
        foreach (var passengerObj in activePassengers.ToArray())
        {
            if (passengerObj == null) continue;

            PassengerNPC passenger = passengerObj.GetComponent<PassengerNPC>();
            if (passenger == null) continue;

            // consume one food per passenger; if not enough food -> damage
            bool ate = rm.ConsumeFood(1);
            if (!ate)
            {
                passenger.TakeDamage(1); 
            }

            if (passenger.IsDead)
            {
                // remove from active list and destroy GameObject
                activePassengers.Remove(passengerObj);
                Destroy(passengerObj);
                Debug.Log($"{passenger.passengerData.passengerName} has died due to starvation or events.");
            }
        }
    }

    // Utility: get count
    public int GetPassengerCount()
    {
        return activePassengers.Count;
    }
}
