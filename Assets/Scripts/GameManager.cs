using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] allAliensSets;

    private GameObject currentSet;
    private Vector2 spawnPos = new Vector2(0,10);
    private static GameManager instance;
    public AudioClip spawnsfx;
    public AudioClip deadfx;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public static void CancelGame()
    {
        instance.StopAllCoroutines();

        AlienMaster.allAliens.Clear();

        if(instance.currentSet != null)
            Destroy(instance.currentSet);

        UIManager.ResetUI();
        AudioManager.StopBattleMusic();
    }
    public static void SpawnNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        AudioManager.UpdateBattleMusic(1);
        AudioManager.StopBattleMusic();
        AlienMaster.allAliens.Clear();
        AudioManager.PlaySoundEffect(deadfx);
        if(currentSet != null)
            Destroy(currentSet);


        yield return new WaitForSeconds(3);

        currentSet = Instantiate(allAliensSets[Random.Range(0, allAliensSets.Length)], spawnPos, Quaternion.identity);
        AudioManager.PlaySoundEffect(spawnsfx);
        UIManager.UpdateWave();
        AudioManager.PlayBattleMusic();
    }
}
