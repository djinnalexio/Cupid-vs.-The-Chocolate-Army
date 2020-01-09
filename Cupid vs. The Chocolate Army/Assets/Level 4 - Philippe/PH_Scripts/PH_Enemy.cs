using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The script that contains the data for each enemy
/// </summary>

public class PH_Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int points = 100;

    [Header("Projectile Settings")]
    [Space(10)]
    [SerializeField] GameObject ProjectilePrefab; // the prefab of the projectile
    [SerializeField] float shootingRateMin;
    [SerializeField] float shootingRateMax;
    [SerializeField] float ProjectileSpeed; // multiplier to tune projectile speed
    float shootingRate;

    [Header("Alternative Form Settings")]
    [Space(10)]
    [SerializeField] [Range(0, 100)] int HealthPercentageFormChange;
    [SerializeField] GameObject AltVisualEffect;
    [SerializeField] [Range(0, 5)] float AltFormSpeedFactor = 0.5f;
    
    [Header("VFX Settings")]
    [Space(10)]
    [SerializeField] GameObject DeathVisualEffect;
    [SerializeField] float ParticleDuration = 3f;
    
    [Header("SFX Settings")]
    [Space(10)]
    [SerializeField] AudioClip DeathSound;
    [SerializeField] [Range(0,1)] float DeathSoundVolume;

    [Header("SFX Projectile Settings")]
    [Space(10)]
    [SerializeField] AudioClip ShootSound;
    [SerializeField] [Range(0,1)] float ShootSoundVolume;

    
    float SpeedMultiplier = 1;
    float HealthFormChange = 0;
    int formIndex = 0;
    Animator EnemyForm;
    
    public float GetHealth() { return health; }
    public int GetPoints() { return points; }
    public float GetSpeedMultiplier() { return SpeedMultiplier; }
    public float GetHealthFormChange() { return HealthFormChange; }

    PH_PlayerController player;

    void Start()
    {
        HealthFormChange = health * HealthPercentageFormChange/100;//the amount of health where change occurs
        shootingRate = UnityEngine.Random.Range(shootingRateMin, shootingRateMax);
        EnemyForm = GetComponent<Animator>();//control of the animation
        player = FindObjectOfType<PH_PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player) CountDownAndShoot();//shoot as long as the player is there
    }

    void CountDownAndShoot()
    {
        shootingRate -= Time.deltaTime;
        if (shootingRate <= 0f)
        {
            Fire();
            shootingRate = UnityEngine.Random.Range(shootingRateMin, shootingRateMax);
        }
    }

    void Fire()//function to fire
    {
        GameObject ChocoBullet = Instantiate(ProjectilePrefab,
            transform.position,
            Quaternion.identity) as GameObject;
        ChocoBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -ProjectileSpeed);
        if (ShootSound)
        {
            AudioSource.PlayClipAtPoint(ShootSound, Camera.main.transform.position, ShootSoundVolume);
        }
    }

    void OnTriggerEnter2D(Collider2D other)//when it's touched by other script
    {
        PH_DamageDealer damageDealer = other.gameObject.GetComponent<PH_DamageDealer>();
        if (!damageDealer) { return; }//if it doesn't have a damage dealer component, just cut it there
        ProcessHit(damageDealer);
    }

    void ProcessHit(PH_DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();//lower health by the amount from the damage dealer script
        damageDealer.Hit();
        if (health <= 0) {Die(); }//if health reahed zero, die

        else if (health <= HealthFormChange && formIndex < 1)//or if health reached the critical point and enemy didn't change form yet
        { formIndex++; ChangeForm(); }//ChangeForm form and increase formIndex
    }

    public void Die()//when enemy dies
    {
        FindObjectOfType<PH_GameSession>().AddToScore(points);//add its points to the score
        Destroy(gameObject);//destroy the object
        GameObject explosion = Instantiate(DeathVisualEffect, transform.position, transform.rotation);//create the explosion
        Destroy(explosion, ParticleDuration);//stop the explosion
        if (DeathSound) { AudioSource.PlayClipAtPoint(DeathSound, Camera.main.transform.position, DeathSoundVolume); }
        //play the death sound if it's there
    }

    void ChangeForm()
    {
        EnemyForm.SetBool("ChangeForm", true);//change the enemy animation
        GameObject change = Instantiate(AltVisualEffect, transform.position, transform.rotation);//create the VFX for the form change
        SpeedMultiplier = AltFormSpeedFactor;//use the new speed multiplier
        Destroy(change, 3f);//stop the VFX
    }

}
