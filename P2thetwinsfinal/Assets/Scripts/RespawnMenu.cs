using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnMenu : MonoBehaviour
{
    public GameObject respawnMenuUI;
    public GameObject optionMenu;
    public GameObject mainMenu;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ShowRespawnMenu()
    {
        Time.timeScale = 0f; // Freeze the game
        respawnMenuUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Unfreeze the game
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the actual scene name of your main menu
        Time.timeScale = 1f; // Unfreeze the game
    }

    public void OpenOptionsMenu()
    {
        optionMenu.SetActive(true);
    }
}
