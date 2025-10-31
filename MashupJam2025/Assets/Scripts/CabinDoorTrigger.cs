using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CabinDoorTrigger : MonoBehaviour
{
    private bool isInRange = false;

    public GameManager gameManager;


    [SerializeField] private TextMeshProUGUI promptText; // Assign in Inspector

    private void Start()
    {
        if (promptText != null)
            promptText.gameObject.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (promptText != null)
                promptText.gameObject.SetActive(true);
        }
    }
  
    private void OnTriggerExit2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        { 
        isInRange = false;
        if (promptText != null)
                promptText.gameObject.SetActive(false);
        }
    }


    private void Update()
    { 
    
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {

            if (promptText != null)
                promptText.gameObject.SetActive(false);
            gameManager.EndCabinPhase();
        }
    }

}
