using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{
    public GameObject respawnMenuUI;
    public GameObject optionMenu; // If you have an options menu
    public GameObject mainMenu; // If you have a main menu

    public void ShowRespawnMenu()
    {
        respawnMenuUI.SetActive(true);
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.Confined; // Confine the cursor within the game window
        Time.timeScale = 0f; // Pause the game

        // Disable other menus if needed
        if (optionMenu != null)
        {
            optionMenu.SetActive(false);
        }
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
        }
    }

    public void HideRespawnMenu()
    {
        respawnMenuUI.SetActive(false);
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Time.timeScale = 1f; // Resume the game

        // Enable other menus if needed
        if (optionMenu != null)
        {
            optionMenu.SetActive(true);
        }
        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Resume the game after respawning
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f; // Resume the game after going to the main menu
    }

    // Add other methods for options menu, etc. if needed
}
