using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Loop Settings")]
    public int totalPhases = 3;
    [HideInInspector] public int currentPhase = 0;

    [Header("Managers")]
    [SerializeField] private PassengerManager passengerManager;
    [SerializeField] private EventManager eventManager;

    [Header("Player Settings")]
    [SerializeField] private GameObject player;

    private bool cabinPhaseActive = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one GameManager found!");
            Destroy(gameObject);
            return;
        }
        instance = this;

        // Spawn the predetermined passengers at initial positions
        passengerManager.SpawnInitialPassengers();
    }

    private void Start()
    {
        StartPhase();
    }

    public void StartPhase()
    {
        if (currentPhase >= totalPhases)
        {
            EndGame();
            return;
        }

        Debug.Log($"Starting Phase {currentPhase + 1}");
        cabinPhaseActive = true;

        EnablePassengerInteractions(true);
    }

    public void EndCabinPhase()
    {
        if (!cabinPhaseActive) return;
        cabinPhaseActive = false;

        EnablePassengerInteractions(false);

        // Trigger Rail Event for current phase
        eventManager.TriggerRailEvent(currentPhase, () =>
        {
            // After Rail Event, trigger Station Event for current phase
            eventManager.TriggerStationEvent(currentPhase, () =>
            {
                currentPhase++;
                if (currentPhase < totalPhases)
                {
                    StartPhase();
                }
                else
                {
                    EndGame();
                }
            });
        });
    }

    private void EnablePassengerInteractions(bool enabled)
    {
        foreach (PassengerNPC npc in passengerManager.activePassengers)
        {
            // Assuming PassengerNPC has a Collider2D for interaction
            Collider2D col = npc.GetComponent<Collider2D>();
            if (col != null) col.enabled = enabled;
        }
    }

    public void AddPassenger(PassengerData data, Vector3 spawnPosition)
    {
        passengerManager.SpawnPassenger(data, spawnPosition);
    }

    public void RemovePassenger(PassengerData data)
    {
        passengerManager.RemovePassenger(data);
    }

    private void EndGame()
    {
        Debug.Log("Game finished!");
        // Here you can add logic to show end screen or return to menu
    }
}
