using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int scoreValue;
    public AudioClip deadfx;
    public GameObject explosion;

    public void Kill()
    {
        UIManager.UpdateScore(scoreValue);
        AlienMaster.allAliens.Remove(gameObject);

        Instantiate(explosion, transform.position, Quaternion.identity);

        AudioManager.UpdateBattleMusic(AlienMaster.allAliens.Count);

        if(AlienMaster.allAliens.Count == 0)
        {
            GameManager.SpawnNewWave();
        }
        AudioManager.PlaySoundEffect(deadfx);
        Destroy(gameObject);
    }
}
