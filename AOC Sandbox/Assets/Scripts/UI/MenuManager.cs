using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
        // Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        // Cursor.visible = isMenuOpen;
    }

    public void ResumeGame()
    {
        isMenuOpen = false;
        menuPanel.SetActive(false);
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Main Scene");
        
    }

    public void LoadGameStart()
    {
        SceneManager.LoadScene("Game Start");
    }

    public void JustClicked()
    {
        Debug.Log("Just clicked");
    }
}