﻿using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour{

    private Transform target;
    private Enemy targetEnemy;


    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public float slowPct = 0f;



    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;





    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget ()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemys)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            //targetEnemy = nearestEnemy.GetComponent<Enemy>();
        } else
        {
            target = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;


        LockOnTarget();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        

        fireCountdown -= Time.deltaTime;

        if (slowPct > 0f)
        {
            Slow();
        
        }


    }

    void LockOnTarget ()
    {
        //Target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //Dreht sich nicht auf der Z-Achse...
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    void Slow ()
    {
        GameObject[] allEnemies;
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            if (Vector2.Distance(enemy.transform.position, this.transform.position) < range)
            {
                enemy.GetComponent<Enemy>().Slow(slowPct);
            }
        }
    }

    void Shoot ()
    {

        

        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);

    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
