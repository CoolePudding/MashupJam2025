using UnityEngine;
using System;
using System.Collections;

public class EventManager : MonoBehaviour
{
    [Header("Rail Event Dialogue Files (Phase 1,2,3)")]
    [SerializeField] private TextAsset[] railEventInk;

    [Header("Station Event Dialogue Files (Phase 1,2,3)")]
    [SerializeField] private TextAsset[] stationEventInk;

    private DialogueManager dialogueManager;

    private void Awake()
    {
        dialogueManager = DialogueManager.GetInstance();
    }

    public void TriggerRailEvent(int phase, Action onComplete = null)
    {
        if (railEventInk == null || railEventInk.Length <= phase || railEventInk[phase] == null)
        {
            Debug.LogWarning($"No Ink file assigned for Rail Event phase {phase}!");
            onComplete?.Invoke();
            return;
        }

        dialogueManager.StartStory(railEventInk[phase]);
        StartCoroutine(WaitForDialogueToEnd(onComplete));
    }

    public void TriggerStationEvent(int phase, Action onComplete = null)
    {
        if (stationEventInk == null || stationEventInk.Length <= phase || stationEventInk[phase] == null)
        {
            Debug.LogWarning($"No Ink file assigned for Station Event phase {phase}!");
            onComplete?.Invoke();
            return;
        }

        dialogueManager.StartStory(stationEventInk[phase]);
        StartCoroutine(WaitForDialogueToEnd(onComplete));
    }

    private IEnumerator WaitForDialogueToEnd(Action onComplete)
    {
        while (dialogueManager.dialogueIsPlaying)
        {
            yield return null;
        }
        onComplete?.Invoke();
    }
}
