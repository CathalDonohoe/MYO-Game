using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    //Declaration of Variables
    public float seconds;

    // Start is called before the first frame update
    void Start()
    {
        //destroys after the seconds
        Destroy(gameObject, seconds);
    }
}
