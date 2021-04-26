using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour
{
    //Declaration of Variables
    private float speed = 10;
   

    void Update()
    {
        //moves bullet up
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    //used for what the bullet collides with
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if collides with alien kills enemy
        if(collision.gameObject.CompareTag("Alien"))
        {
            collision.gameObject.GetComponent<Alien>().Kill();
            Destroy(gameObject);
        }

        //destorys enemy bullet if hit
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
