using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class Player : MonoBehaviour
{
    public ShipStats shipStats;
    public GameObject bulletPrefab;
    public AudioClip shootsfx;
    public AudioClip deadfx;

    private Vector2 offScreenPos = new Vector2(0, -20f);
    private Vector2 startingPos = new Vector2(0, -4.5f);

    private const float MAX_LEFT = -3.4f;
    private const float MAX_RIGHT = 3.4f;

    private float speed = 3;
    private float coolDown = 0.5f;

    private bool isShooting;
    private bool isPaused = false;

    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;    


    // The pose from the last update. This is used to determine if the pose has changed
    // so that actions are only performed upon making them rather than every frame during
    // which they are active.
    private Pose _lastPose = Pose.Unknown;

    // Update is called once per frame.

    private void Start()
    {
        shipStats.currentHealth = shipStats.maxzHealth;
        shipStats.currentLives = shipStats.maxLives;

        Debug.Log(shipStats.currentHealth);

        transform.position = startingPos;

        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLives);
    }


    void Update()
    {
// Access the ThalmicMyo component attached to the Myo game object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
         if (thalmicMyo.pose != _lastPose) {
            _lastPose = thalmicMyo.pose;
            if(Input.GetKey(KeyCode.A) && transform.position.x > MAX_LEFT || thalmicMyo.pose == Pose.WaveOut && transform.position.x > MAX_LEFT){
                transform.Translate(Vector2.left * 0.2f * shipStats.shipSpeed);
                ExtendUnlockAndNotifyUserAction (thalmicMyo);

            }

            else if(Input.GetKey(KeyCode.D)&& transform.position.x < MAX_RIGHT || thalmicMyo.pose == Pose.WaveIn && transform.position.x < MAX_RIGHT){
                transform.Translate(Vector2.right * 0.2f * shipStats.shipSpeed);
                ExtendUnlockAndNotifyUserAction (thalmicMyo);
            }

            else if(Input.GetKey(KeyCode.Space) && !isShooting || thalmicMyo.pose == Pose.DoubleTap  && !isShooting){
                StartCoroutine(Shoot());
            }

            else if(thalmicMyo.pose == Pose.FingersSpread)
            {
                Debug.Log("finger spread");
                MenuManager.OpenPauseMyo();
                isPaused  = true;
                //ExtendUnlockAndNotifyUserAction (thalmicMyo);
            }
            else if(thalmicMyo.pose == Pose.Fist)
            {
                Debug.Log("fist");
                MenuManager.ClosePauseMyo();
                isPaused = false;
                //ExtendUnlockAndNotifyUserAction (thalmicMyo);
            }
         }

         

        // Check if the pose has changed since last update.
        // The ThalmicMyo component of a Myo game object has a pose property that is set to the
        // currently detected pose (e.g. Pose.Fist for the user making a fist). If no pose is currently
        // detected, pose will be set to Pose.Rest. If pose detection is unavailable, e.g. because Myo
        // is not on a user's arm, pose will be set to Pose.Unknown.
        // if (thalmicMyo.pose != _lastPose) {
        //     _lastPose = thalmicMyo.pose;

        //     // Vibrate the Myo armband when a fist is made.
        //     if (thalmicMyo.pose == Pose.Fist) {
        //         thalmicMyo.Vibrate (VibrationType.Medium);
        //         Debug.Log("Fist");
        //         ExtendUnlockAndNotifyUserAction (thalmicMyo);

        //     // Change material when wave in, wave out or double tap poses are made.
        //     } else if (thalmicMyo.pose == Pose.WaveIn && transform.position.x > MAX_LEFT) {

        //         //transform.Translate(Vector2.right * 0.3f * shipStats.shipSpeed);
        //         ExtendUnlockAndNotifyUserAction (thalmicMyo);
        //         Debug.Log("Wave in");
        //     } else if (thalmicMyo.pose == Pose.WaveOut && transform.position.x < MAX_RIGHT) {
                
        //         Debug.Log("wave out");
        //         //transform.Translate(Vector2.left * 0.3f * shipStats.shipSpeed);
        //         ExtendUnlockAndNotifyUserAction (thalmicMyo);
        //     } else if (thalmicMyo.pose == Pose.DoubleTap  && !isShooting) {
               
        //         Debug.Log("double tap");
        //         StartCoroutine(Shoot());
        //         ExtendUnlockAndNotifyUserAction (thalmicMyo);
        //     }
        // }


    }

    private void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        
        if(shipStats.currentHealth <= 0)
        {
            shipStats.currentLives--;
            UIManager.UpdateLives(shipStats.currentLives);

            if(shipStats.currentLives <= 0)
            {
                //Game over
                Debug.Log("Game Over");
            }

            else{
                //respawn
                // Debug.Log("Respawn");
                StartCoroutine(Respawn());
            }
        }

    }


    private IEnumerator Shoot(){
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        AudioManager.PlaySoundEffect(shootsfx);
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPos;
        AudioManager.PlaySoundEffect(deadfx);

        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxzHealth;

        transform.position = startingPos;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player hit");
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }


    // Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
    // recognized.
    void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (hub.lockingPolicy == LockingPolicy.Standard) {
            myo.Unlock (UnlockType.Timed);
        }

        myo.NotifyUserAction ();
    }
}
