using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    //Declaration of Variables
    public int scoreValue;
    public AudioClip deadfx;
    public GameObject explosion;

    public void Kill()
    {
        //updates score
        UIManager.UpdateScore(scoreValue);
        //removes current alien from alien array
        AlienMaster.allAliens.Remove(gameObject);
        //spawns explosion
        Instantiate(explosion, transform.position, Quaternion.identity);
        //speeds up alien sound
        AudioManager.UpdateBattleMusic(AlienMaster.allAliens.Count);

        //if all aliens are dead
        if(AlienMaster.allAliens.Count == 0)
        {
            //calls method to spwan new method
            GameManager.SpawnNewWave();
        }
        //play sound
        AudioManager.PlaySoundEffect(deadfx);
        //destorys alien
        Destroy(gameObject);
    }
}
