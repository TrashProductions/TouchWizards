﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AI_Controls : MonoBehaviour
{
    public static float MoveSpeed = 1;
    public static float Health = 1;
    Rigidbody2D rb2d;
    public Slider HealthSlider;
    public GameObject ShootingPoint;
    public GameObject Bullet;
    public GameObject ShieldPrefab;
    public Transform ShieldPoint;
    public static int NumberOfShields;
    List<float> BoosterXPos = new List<float>();
    public bool DetectedEnemyBullet;
    public bool DetectedEnemy;
    bool hasChanged;
    public static bool ReadyToShoot;
    public LayerMask P1_Bullet;
    public LayerMask Enemy;
    public static List<GameObject> Shields = new List<GameObject>();
    public float BulletDetectionRaduis;
    float random;
    float randomDuration;


    void Start()
    {
        hasChanged = false;
        rb2d = GetComponent<Rigidbody2D>();
        BoosterXPos.Add(-5f);
        BoosterXPos.Add(5f);
        NumberOfShields = 2;
        Health = 1;
        random = Random.Range(0, 110);
        randomDuration = Random.Range(4, 30);
        ReadyToShoot = true;
        if (PlayerPrefs.GetInt("CurrentScore") != 0)
        {
            if (MoveSpeed < 7)
            {
                MoveSpeed = PlayerPrefs.GetInt("CurrentScore");
            }

        }

        if (Health <= 4 && PlayerPrefs.GetInt("CurrentScore") != 0)
        {

            Health = PlayerPrefs.GetInt("CurrentScore") / 1.4f;
            HealthSlider.maxValue = Health;
        }
    }

    void Update()
    {
        DetectedEnemyBullet = Physics2D.OverlapCircle(transform.position, BulletDetectionRaduis, P1_Bullet);
        DetectedEnemy = Physics2D.OverlapArea(transform.position, new Vector2(transform.position.x, transform.position.y - 60f), Enemy);
        Loop();
        HealthStatus();
        Movement();
        Shield();
        Shoot();
    }

    public void Shield()
    {
        if (DetectedEnemyBullet)
        {
            if (NumberOfShields <= 1 * PlayerPrefs.GetInt("CurrentScore") && NumberOfShields < 5)
            {
                Shields.Add(Instantiate(ShieldPrefab, ShieldPoint.position, Quaternion.identity) as GameObject);
                NumberOfShields++;
            }
        }
    }

    public void Shoot()
    {
        if (DetectedEnemy && ReadyToShoot)
        {
            GameObject BulletClone;
            BulletClone = Instantiate(Bullet, ShootingPoint.transform.position, Quaternion.Euler(0, 0, -90f)) as GameObject;
            DetectedEnemy = false;
            ReadyToShoot = false;
        }
    }

    public void HealthStatus()
    {
        HealthSlider.value = Health;
        if (Health <= 0)
        {
            if (!hasChanged)
                PlayerPrefs.SetInt("CurrentScore", PlayerPrefs.GetInt("CurrentScore") + 1);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            SoloGameManager.GameOver.SetActive(true);
            SoloGameManager.replaytext = "Next";
            SoloGameManager.P1_GameOverText = "You Win!";
            GetComponent<SpriteRenderer>().DOFade(0, 2f);
            SoloGameManager.GameOn.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void Movement()
    {
        if (random > 30 && random < 60)
        {
            rb2d.velocity = new Vector2(MoveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
<<<<<<< HEAD
            GetComponent<Animator>().SetBool("Player2_isRunning", true);
=======
            GetComponent<Animator>().SetBool("P2_isRunning", true);
>>>>>>> V2
            randomDuration++;
            if (randomDuration >= 30)
            {
                random = Random.Range(0, 110);
                randomDuration = Random.Range(4, 30);
            }
        }
        else if (random > 60 && random < 100)
        {
            rb2d.velocity = new Vector2(-MoveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
<<<<<<< HEAD
            GetComponent<Animator>().SetBool("Player2_isRunning", true);
=======
            GetComponent<Animator>().SetBool("P2_isRunning", true);
>>>>>>> V2
            randomDuration++;
            if (randomDuration >= 30)
            {
                random = Random.Range(0, 110);
                randomDuration = Random.Range(4, 30);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(0, 0);
<<<<<<< HEAD
            GetComponent<Animator>().SetBool("Player2_isRunning", false);
=======
            GetComponent<Animator>().SetBool("P2_isRunning", false);
>>>>>>> V2
            randomDuration++;
            if (randomDuration >= 30)
            {
                random = Random.Range(0, 110);
                randomDuration = Random.Range(4, 30);
            }
        }
    }

    public void Loop()
    {
        if (transform.position.x <= -3)
        {
            transform.position = new Vector2(-(transform.position.x + 0.1f), transform.position.y);
        }
        else if (transform.position.x >= 3)
        {
            transform.position = new Vector2(-(transform.position.x - 0.1f), transform.position.y);
        }
    }
}
