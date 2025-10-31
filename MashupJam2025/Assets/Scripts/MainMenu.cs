using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu References")]
    [SerializeField] private GameObject BackGround;
    [SerializeField] private GameObject creditsbg;

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        creditsbg.SetActive(true);
    }

    public void ReturnToMenu()
    {
        creditsbg.SetActive(false);
        BackGround.SetActive(true);
    }
}