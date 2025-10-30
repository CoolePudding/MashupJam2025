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
        return allPassengers.Find(p => p.passengerName == name);
    }
}
