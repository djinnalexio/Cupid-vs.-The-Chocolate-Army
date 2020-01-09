using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JH_BigEnemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 800;


    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;

    Animator GiantMelt;

    // Start is called before the first frame update
    void Start()
    {
        GiantMelt = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        JH_DamageDealer damageDealer = other.gameObject.GetComponent<JH_DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(JH_DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
    }

    // Update is called once per frame
    void Update()
    {               
        CountDownAndShoot();

        if (health <= 0)
        {
            GiantMelt.SetBool("isHit", true);
            Die();
        }
    }
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject glob = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        glob.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }

    private void Die()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject, 0.5f);
        FindObjectOfType<JH_GameSession>().AddToScore(scoreValue);
    }
}