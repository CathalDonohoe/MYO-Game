using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameOverMenu;
    public GameObject inGameMenu;
    public GameObject pauseMenu;
    public AudioClip pausesfx;
    public AudioClip unpausesfx;
    public AudioClip menusfx;

    public static MenuManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OpenMainMenu()
    {
        instance.mainMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    public static void OpenGameOver()
    {
        instance.gameOverMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    public void OpenInGame()
    {
        Time.timeScale = 1;
        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);

        GameManager.SpawnNewWave();
    }

    public static void OpenInGameSpeech()
    {
        Time.timeScale = 1;
        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);

        GameManager.SpawnNewWave();
        return;
    }

    private void Start()
    {
        ReturnToMainMenu();
    }
    public void OpenPause()
    {
        AudioManager.PlaySoundEffect(pausesfx);
        instance.inGameMenu.SetActive(false);
        instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public static void OpenPauseMyo()
    {
        instance.inGameMenu.SetActive(false);
        instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReturnToMainMenu()
    {
        AudioManager.PlaySoundEffect(menusfx);
        Time.timeScale = 1;
        instance.gameOverMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.inGameMenu.SetActive(false);
        instance.mainMenu.SetActive(true);
        GameManager.CancelGame();
    }

    public void ClosePause()
    {
        AudioManager.PlaySoundEffect(unpausesfx);
        instance.inGameMenu.SetActive(true);
        instance.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public static void ClosePauseMyo()
    {
        instance.inGameMenu.SetActive(true);
        instance.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public static void CloseWindow(GameObject go)
    {
        go.SetActive(false);
    }
}
