using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a method used for controlling the audio in game
public class AudioManager : MonoBehaviour
{
    //Declaration of Variables
    private static AudioManager instance;
    private bool muted;
    public AudioSource battleMusicSource;
    public AudioSource sfxSource;

    private bool isPlaying;
    private float delay;
    private const float DELAY_TICK = 0.05f;

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
    

    private void Start()
    {
        muted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = muted;
    }

    public void ToggleMute()
    {
        muted = !muted;

        AudioListener.pause = muted;
        PlayerPrefs.SetInt("MUTED", muted ? 1 : 0);
    }
    

    //mehtod to play enemy sound
    public static void PlayBattleMusic()
    {
        instance.delay = 1;
        
        instance.isPlaying = true;
        instance.StartCoroutine(instance.BattleSound());
    }

    //method to stop the enemy sound
    public static void StopBattleMusic()
    {
        instance.isPlaying = false;
        instance.StopCoroutine(instance.BattleSound());
    }

    //plays a sound effect
    public static void PlaySoundEffect(AudioClip clip)
    {
        if(!instance.muted)
            instance.sfxSource.PlayOneShot(clip);

        
    }

    //makes sound play quicker with less enemies
    public static void UpdateBattleMusic(int i)
    {
        float delayTime = i * DELAY_TICK;

        if(delayTime < 0.2f)
            delayTime = 0.2f;

        if(delayTime > 1)
            delayTime = 1;

        instance.delay = delayTime;
    }

    //coroutine for playing enemy sound to form music
    private IEnumerator BattleSound()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(delay);
            battleMusicSource.Play();
        }
    }

}
