using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.Confined; // Confine the cursor within the game window
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.Confined; // Confine the cursor within the game window
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.Confined; // Confine the cursor within the game window
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
