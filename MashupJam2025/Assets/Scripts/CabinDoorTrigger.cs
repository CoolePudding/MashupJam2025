using UnityEngine;
using UnityEngine.SceneManagement;

public class CabinDoorTrigger : MonoBehaviour
{
    private bool isInRange = false;

    public GameManager gameManager;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            // Optional: show UI prompt "Press E to enter"
        }
    }
  
    private void OnTriggerExit2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        { 
        isInRange = false;
        }
    }


    private void Update()
    { 
    
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            gameManager.EndCabinPhase();
        }
    }

}
