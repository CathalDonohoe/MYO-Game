using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    //Declaration of Variables
    public Sprite[] states;

    public static int health;
    private SpriteRenderer sr;

    void Awake()
    {
        health = 4;    
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = states[health];
    }

    //shifts number in array, causing sprite to change
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            health--;

            if(health <= 0)
                Destroy(gameObject);
            else 
                sr.sprite = states[health-1];
        }

        if(collision.gameObject.CompareTag("FriendlyBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
