using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Declaration of Variables
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


    //opens menu UI
    public void OpenMainMenu()
    {
        instance.mainMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    //opens game over UI
    public static void OpenGameOver()
    {
        instance.gameOverMenu.SetActive(true);
        instance.inGameMenu.SetActive(false);
    }

    //opens game UI
    public void OpenInGame()
    {
        Time.timeScale = 1;
        instance.mainMenu.SetActive(false);
        instance.pauseMenu.SetActive(false);
        instance.gameOverMenu.SetActive(false);
        instance.inGameMenu.SetActive(true);

        GameManager.SpawnNewWave();
    }

    //opens in game UI for voice recognition
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

    //opens pause UI
    public void OpenPause()
    {
        AudioManager.PlaySoundEffect(pausesfx);
        instance.inGameMenu.SetActive(false);
        instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    //opens pause UI for voice recognition
    public static void OpenPauseMyo()
    {
        instance.inGameMenu.SetActive(false);
        instance.pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    //opens menu UI
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

    //close pause UI
    public void ClosePause()
    {
        AudioManager.PlaySoundEffect(unpausesfx);
        instance.inGameMenu.SetActive(true);
        instance.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    //close pause UI with MYO
    public static void ClosePauseMyo()
    {
        instance.inGameMenu.SetActive(true);
        instance.pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }


    //Closes a UI window
    public static void CloseWindow(GameObject go)
    {
        go.SetActive(false);
    }

    //restarts scene
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
