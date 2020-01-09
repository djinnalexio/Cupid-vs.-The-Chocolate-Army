using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script that controls the player and holds player data
/// </summary>

public class PH_PlayerController : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] float PlayerSpeed; // multiplier to tune player speed
    [SerializeField] float Padding; // to keep the whole player on screen
    [SerializeField] float startingHealth = 100;
    float health;


    [Header("Projectile")]
    [Space(10)]
    [SerializeField] GameObject ArrowPrefab; // the prefab of the arrow projectile
    [SerializeField] float ArrowSpeed; // multiplier to tune arrow speed
    [SerializeField] float ShootingRate; // to tune shooting rate
    [SerializeField] AudioClip ShootSound;
    [SerializeField] [Range(0, 1)] float ShootSoundVolume;


    [Header("Player VFX Settings")]
    [Space(10)]
    [SerializeField] GameObject DeathEffect;
    [SerializeField] float ParticleDuration = 3f;


    [Header("Player SFX Settings")]
    [Space(10)]
    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range(0, 1)] float DeathSoundVolume;

    //external scripts
    PH_EnemySpawner Spawner;
    PH_Pause PauseController;
    PH_GameSession gameSession;
    PH_SceneLoader sceneLoader;

    //firing variables
    Animator BowMovement;
    Coroutine ContinuousFireOn;
    

    //the max and min coordinates of the player relative to the camera
    float Xmin;
    float Xmax;

    public float GetHealth() { return health; }
    public float GetstartingHealth() { return startingHealth; }

    void Start()
    {
        CameraBounderies();

        BowMovement = GetComponent<Animator>();//to control the bow animation

        Spawner = FindObjectOfType<PH_EnemySpawner>();//find all those elements in the scene
        PauseController = FindObjectOfType<PH_Pause>();
        gameSession = FindObjectOfType<PH_GameSession>();
        sceneLoader = FindObjectOfType<PH_SceneLoader>();

        float healthBonus = ((gameSession.GetMaxLives() - gameSession.GetCurrentLives()) * 1f / gameSession.GetMaxLives()) / 2;
        health = startingHealth *= (1 + healthBonus);//calculate bonus health, +10% for each lost heart
    }

    void Update()
    {
        Move();

        if (!PauseController.GetPauseStatus()) { Fire(); }//can't fire while in pause
    }

    void CameraBounderies() //define the boundaries depending on the camera
    {
        Camera GameCamera = Camera.main;
        Xmin = GameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + Padding;
        Xmax = GameCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x - Padding;
    }

    void Move()
    {
        var Xmovecommand = Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed;//Get the input value from the keys that affect the x-axis
        //Time.deltatime is the time it took to complete the previous frame.
        //PlayerSpeed is a variable multiplier that allows us to tune the speed of the player.

        var newXposition = transform.position.x + Xmovecommand;
        //creates the new position the player by adding the input to the current position

        newXposition = Mathf.Clamp(newXposition, Xmin, Xmax);//limits values to keep player on screen
        transform.position = new Vector3(newXposition, transform.position.y, transform.position.z);//moves the player
    }

    void Fire()//function to fire
    {
        if (Input.GetButtonDown("Fire1"))//open fire
        {//assign the coroutine to a variable so that we can refer to it later
            BowMovement.SetBool("Shooting", true);//meets the condition for the animation to transition
            ContinuousFireOn = StartCoroutine(ContinuousFire());
        }
        if (Input.GetButtonUp("Fire1"))// cease fire
        {
            BowMovement.SetBool("Shooting", false);//allows animation to go back idle
            if (ContinuousFireOn != null) StopCoroutine(ContinuousFireOn);//stop shooting if it was shooting.
        }
    }

    public void ShootingKillSwitch()//We need that there so that the playin isn't stuck on shooting after resuming
    {
        if (ContinuousFireOn != null)
        {
            BowMovement.SetBool("Shooting", false);
            StopCoroutine(ContinuousFireOn);
        }
    }
    
    IEnumerator ContinuousFire()// creates a coroutine
    {
        while (true)
        {
            GameObject Arrow = Instantiate(ArrowPrefab,//create the get object from the arrow prefab
                transform.position, //assigns its position as default
                Quaternion.identity) as GameObject; // set the rotation as default
            Arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0, ArrowSpeed);
            //the rigid body is set to kinetic so that it's not affected by gravity and is given a speed
            AudioSource.PlayClipAtPoint(ShootSound, Camera.main.transform.position, ShootSoundVolume);
            yield return new WaitForSeconds(ShootingRate);//time between shots
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PH_DamageDealer damageDealer = other.GetComponent<PH_DamageDealer>();//get the script from the object
        if (!damageDealer) { return; }//if there's nothing, just move on
        ProcessHit(damageDealer);// use the script
    }

    void ProcessHit(PH_DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();//take damage
        damageDealer.Hit();//will destroy the foreign game object
        if (health <= 0) { Die(); }
    }

    void Die()
    {
        Spawner.SetWaveLooping(false);// stop spawing enemies while player is dead

        Destroy(gameObject);//remove player
        
        BowBreak();//player death animation

        gameSession.LoseALife();//process for losing a life

        if (gameSession.GetCurrentLives() >= 1) { sceneLoader.ShowRetryScreen(); }
        //if some lives left, try again option, else, go to game over scene
        else { sceneLoader.LoadGameOverScene(); }
        
    }

    public void TransitionToWinScreen()
    {
        this.GetComponent<Collider2D>().isTrigger = false;//when goal is reach, disable the collider to make the player invinsible
        Spawner.SetWaveLooping(false);//stop summoning enemies
        sceneLoader.ShowWinTransition();
    }

    void BowBreak()
    {
        GameObject explosion = Instantiate(DeathEffect, transform.position, transform.rotation);//create the particle effect
        Destroy(explosion, ParticleDuration);//stop it after ParticleDuration seconds
        if (DeathSound) { AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, DeathSoundVolume); }//if there's a sound, play it;
    }//it's really here just so that I wouldn't need to wait to find the sound effects before testing the rest of the game

}
