using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShip : MonoBehaviour
{
    //Declaration of Variables
    public int scoreValue;

    private const float MAX_LEFT = -5f;
    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        //moces the mothership right to left
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if(transform.position.x <= MAX_LEFT)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //destroys if hit by player
        if(collision.gameObject.CompareTag("FriendlyBullet"))
        {
            UIManager.UpdateScore(scoreValue);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
