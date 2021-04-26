﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMaster : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject motherShip;

    public AudioClip shootsfx;
    private Vector3 hMoveDistance = new Vector3(0.05f,0,0);
    private Vector3 vMoveDistance = new Vector3(0,0.15f,0);
    private Vector3 mostherShipSpawnPos = new Vector3(3.72f,3.45f,0);
    

    private const float MAX_LEFT = -3.4f;
    private const float MAX_RIGHT = 3.4f;
    private const float MAX_MOVE_SPEED = 0.02f;
    private const float START_Y = 1.15f;


    private float moveTimer = 0.01f;
    private const float MOVE_TIME = 0.005f;

    private float shootTimer = 3f;
    private const float SHOOT_TIME = 3f;

    private float motherShipTimer = 60f;
    private const float MOTHERSHIP_MIN = 15f;
    private const float MOTHERSHIP_MAX = 60f;

    private bool movingRight;
    private bool entering = true;

    public static List<GameObject> allAliens = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Alien"))
            allAliens.Add(go);
    }

    // Update is called once per frame
    void Update()
    {
        if(entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);

            if(transform.position.y  <= START_Y)
            {
                entering = false;
            }
        }
        else
        {
            if(moveTimer <= 0)
            MoveEnemies();

            if(shootTimer <= 0)
                Shoot();

            if(motherShipTimer <= 0)
            {
                SpawnMothership();
            }
            
            moveTimer -= Time.deltaTime;
            shootTimer -= Time.deltaTime;
            motherShipTimer -= Time.deltaTime;
        }
        
        
    }


    private void MoveEnemies()
    {
        int hitMax = 0;
        if(allAliens.Count > 0){
            for (int i = 0; i < allAliens.Count; i++)
            {
                if(movingRight){
                    allAliens[i].transform.position += hMoveDistance;
                }
                else{
                    allAliens[i].transform.position -= hMoveDistance;
                }

                if(allAliens[i].transform.position.x > MAX_RIGHT || allAliens[i].transform.position.x < MAX_LEFT)
                {
                    hitMax ++;
                }
            }

            if(hitMax > 0){
                for (int i = 0; i < allAliens.Count; i++)
                {
                    allAliens[i].transform.position -= vMoveDistance;
                }

                movingRight = !movingRight;
            }

            moveTimer = GetMoveSpeed();
        }
    }

    private float GetMoveSpeed()
    {
        float f = allAliens.Count * MOVE_TIME;

        if(f<MAX_MOVE_SPEED){
            return MAX_MOVE_SPEED;
        }
        else
        {
            return f;
        }
        

    }


    private void Shoot()
    {
        Vector2 pos = allAliens[Random.Range(0, allAliens.Count)].transform.position;
        AudioManager.PlaySoundEffect(shootsfx);
        Instantiate(bulletPrefab,pos,Quaternion.identity);

        shootTimer = SHOOT_TIME;
    }    
    

    public void SpawnMothership()
    {
        Instantiate(motherShip, mostherShipSpawnPos, Quaternion.identity);
        motherShipTimer = Random.Range(MOTHERSHIP_MIN, MOTHERSHIP_MAX);
    }
}
