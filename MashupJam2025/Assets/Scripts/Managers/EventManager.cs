using UnityEngine;
using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    [System.Serializable]
    public class GameEvent
    {
        public string title;
        [TextArea(2, 5)] public string description;
        public int scrapChange;
        public int foodChange;
        public int gasolineChange;
        public int trainDamage;
    }

    public List<GameEvent> travelEvents;

    public void TriggerRandomTravelEvent(Action onComplete)
    {
        GameEvent e = travelEvents[UnityEngine.Random.Range(0, travelEvents.Count)];
        Debug.Log($"Event: {e.title} — {e.description}");

        ApplyEventEffects(e);
        onComplete?.Invoke();
    }

    private void ApplyEventEffects(GameEvent e)
    {
        var rm = GameManager.Instance.resourceManager;

        rm.AddScraps(e.scrapChange);
        rm.food = Mathf.Clamp(rm.food + e.foodChange, 0, rm.maxFood);
        rm.gasoline = Mathf.Clamp(rm.gasoline + e.gasolineChange, 0, rm.maxGasoline);
        rm.DamageTrain(e.trainDamage);
    }
}
