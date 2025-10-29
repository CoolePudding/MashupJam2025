using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Managers")]
    public ResourceManager resourceManager;
    public PassengerManager passengerManager;
    public EventManager eventManager;
    public StationManager stationManager;

    [Header("Game State")]
    public int currentStationIndex = 0;
    public bool isTraveling = false;

    public event Action OnTravelStart;
    public event Action OnTravelEnd;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        resourceManager.InitializeResources();
        passengerManager.SpawnInitialPassengers(5);
        stationManager.EnterStation(currentStationIndex);
    }

    public void StartTravel()
    {
        if (resourceManager.ConsumeGasoline(1))
        {
            isTraveling = true;
            OnTravelStart?.Invoke();

            // Random travel event
            eventManager.TriggerRandomTravelEvent(() =>
            {
                // When event is done:
                EndTravel();
            });
        }
        else
        {
            Debug.Log("Not enough gasoline to travel!");
        }
    }

    public void EndTravel()
    {
        isTraveling = false;
        currentStationIndex++;
        OnTravelEnd?.Invoke();

        // Arrive at new station
        stationManager.EnterStation(currentStationIndex);
    }
}
