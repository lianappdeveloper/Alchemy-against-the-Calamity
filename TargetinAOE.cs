using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetinAOE : MonoBehaviour {

    [Header("stats")]
    public float RangeAreoundTarget = 2f; 
    public float range = 10f;
    public float fireCD = 2f;
    private float bulspd = 0f;
    public AnimationCurve bularc;
    //Add second fire point.
    public bool SecondFirePoint = false;
    public TowerStats Stats;
    [Header("fields")]
    public bool CanHitEnemyWithFlying;
    public Transform target;
    public GameObject ProjectailPrefab;
    public Transform firePoint, firePoint2;
    public static string EnemyTag = "Enemy";
    public GameObject Enemy;
    public float ClosestEnemy;
    public float ClosestTarget = 0;
    private int ShootCounter = 0;




    void Start()
    {

        Stats = this.gameObject.GetComponent<TowerStats>();
        InvokeRepeating("UpdateTarget", 0f, 0.1f);

    }

    void UpdateTarget()
    {
        bulspd -= Time.deltaTime;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        //where the enemy is
        float enemyPlace = Mathf.Infinity;
        GameObject TargetEnemy = null;
        foreach (GameObject Enemy in enemies)
        {
            EnemyPath E_Path = Enemy.GetComponent<EnemyPath>();
            bool IsEnemyFlying = Enemy.GetComponent<EnemyStats>().Flying;
            float distanceToEnemy = Vector2.Distance(transform.position, Enemy.transform.position);

            if (distanceToEnemy <= range)
            {
                enemyPlace = distanceToEnemy;
                ClosestEnemy = E_Path.distanceTravelled;
                if (ClosestEnemy > ClosestTarget)
                {
                    if (CanHitEnemyWithFlying == false && IsEnemyFlying == true)
                    {
                        TargetEnemy = null;
                        target = null;
                    }
                    else
                        TargetEnemy = Enemy;


                }
                if (enemyPlace > distanceToEnemy)
                {
                    TargetEnemy = null;
                    ClosestTarget = 0;
                }
            }
            else
            {
                TargetEnemy = null;
                target = null;
            }


            if (TargetEnemy != null)
            {
                target = Enemy.transform;
                if (bulspd <= 0f)
                {
                    ShootCounter++;
                    Shoot();
                    if (ShootCounter >= Stats.ammo_Capacity)
                    {
                        bulspd = Stats.Reload;
                        ShootCounter = 0;
                    }
                    else
                    {
                        bulspd = fireCD;
                    }
                }
            }
        }

    }

    void Shoot()
    {
        
        GameObject bulletGO = (GameObject)Instantiate(ProjectailPrefab, firePoint.position, firePoint.rotation);
        projectile bullet = bulletGO.GetComponent<projectile>();
        if (SecondFirePoint == true)
        {
            GameObject bulletGO2 = (GameObject)Instantiate(ProjectailPrefab, firePoint2.position, firePoint2.rotation);
            projectile bullet2 = bulletGO2.GetComponent<projectile>();
            Stats.Shoot(bullet2, target, bularc);
        }
        if (bullet != null)
        {
            Stats.Shoot(bullet, target, bularc);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
