using UnityEngine;

public class StationManager : MonoBehaviour
{
    public void EnterStation(int stationIndex)
    {
        Debug.Log($"Arrived at station #{stationIndex}");

        GameManager.Instance.passengerManager.OnArriveAtStation();

        // Small chance of event
        if (Random.value < 0.2f)
        {
            GameManager.Instance.eventManager.TriggerRandomTravelEvent(() =>
            {
                Debug.Log("Station event finished.");
            });
        }

        // TODO: Open shop UI here
    }
}
