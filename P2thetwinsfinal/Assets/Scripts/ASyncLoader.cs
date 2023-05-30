using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ASyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(StartLoadingProcess(levelToLoad));
    }

    IEnumerator StartLoadingProcess(string levelToLoad)
    {
        yield return new WaitForSeconds(1f); // Delay before starting the loading process

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false; // Don't immediately activate the loaded scene

        float targetProgress = 0.9f; // Target progress value to reach quickly
        float currentProgress = 0f; // Current progress value

        while (currentProgress < targetProgress)
        {
            currentProgress += 0.02f; // Increment the progress more quickly
            loadingSlider.value = currentProgress;

            if (currentProgress >= 0.3f && currentProgress <= 0.7f)
            {
                yield return new WaitForSeconds(Random.Range(0.3f, 0.7f)); // Random freeze time between 0.3 and 0.7 seconds
            }
            else
            {
                yield return new WaitForSeconds(0.02f); // Delay between increments
            }
        }

        loadingSlider.value = 1f; // Set the slider to the maximum value

        loadOperation.allowSceneActivation = true; // Activate the loaded scene
    }
}
