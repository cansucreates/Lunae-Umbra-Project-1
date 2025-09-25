using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ClosePanels()
    {
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
