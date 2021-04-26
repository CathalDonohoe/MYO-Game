using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Declaration of Variables
    private float speed = 10;
    
    void Update()
    {
        //moves bullet down straight
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

}
