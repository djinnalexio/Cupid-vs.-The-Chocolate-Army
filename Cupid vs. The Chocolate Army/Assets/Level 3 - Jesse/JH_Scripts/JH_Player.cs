using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JH_Player : MonoBehaviour {

    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 750;
    

    [Header("Projectile")]
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;
    Animator L3Bow;


    float xMin;
    float xMax;
 
    // Use this for initialization
    void Start ()
    {       
        SetUpMoveBoundaries();
        L3Bow = GetComponent<Animator>();     
    }

    void Update ()
    {
        Move();
        Fire();

        if (health <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("PH_Game Over");
            }
        else
        { }
        
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
        if (health <= 0)
        {
            Destroy(gameObject);
            FindObjectOfType<JH_Level>().LoadGameOver();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
            L3Bow.SetBool("isShooting", true);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
            L3Bow.SetBool("isShooting", false);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as GameObject;
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }


    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; 
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        transform.position = new Vector3(newXPos, transform.position.y);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;     
    }

    
}
