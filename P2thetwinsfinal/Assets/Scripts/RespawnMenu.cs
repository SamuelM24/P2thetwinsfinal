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
    }

    public void HideRespawnMenu()
    {
        respawnMenuUI.SetActive(false);
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
