using System.Collections;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    [Header("stats")]
    public float range = 10f;
    public float fireCD = 0f;
    public float fireCD2 = 0f;
    public float fireCDStun = 0f;
    private float bulspd = 0f;
    private float bulspd2 = 0f;
    private float stunningBulletspeed = 0f;
    public AnimationCurve bularc;
    public AnimationCurve bularc2;
    public AnimationCurve stunningBulletarc;
    //Add second fire point.
    public bool SecondFirePoint = false;
    //add Stun Fire Point
    public bool StunFirePoint = false;
    public TowerStats Stats;
    [Header("fields")]
    public bool CanHitEnemyWithFlying;
    public Transform target;
    public GameObject ProjectailPrefab;
    public GameObject ProjectailPrefab2;
    public GameObject ProjectailStunPrefab;
    public Transform firePoint, firePoint2,stunFirePoint;
    public static string EnemyTag = "Enemy";
    public GameObject Enemy;
    public float ClosestEnemy;
    public float ClosestTarget = 0;
    private int ShootCounter = 0;
    private int ShootSecondCounter = 0;
    private int StunsecondCounter = 0;
    
    void Start()
    {
        Stats = this.gameObject.GetComponent<TowerStats>();
    }

     void Update()
    {
        bulspd -= Time.deltaTime;
        bulspd2-= Time.deltaTime;
        stunningBulletspeed -= Time.deltaTime;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        //where the enemy is
        float enemyPlace = Mathf.Infinity;
        GameObject TargetEnemy = null;
        foreach (GameObject Enemy in enemies)
        {
            EnemyPath E_Path = Enemy.GetComponent<EnemyPath>();
            bool IsEnemyFlying = Enemy.GetComponent<EnemyStats>().Flying;
            float distanceToEnemy = Vector2.Distance(transform.position, Enemy.transform.position);

            if (distanceToEnemy <= range )
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
                if (bulspd2 <= 0f&&SecondFirePoint==true)
                {
                    ShootSecondCounter++;
                    ShootSecondFirePoint();

                    if (ShootSecondCounter >= Stats.ammo_Capacity2)
                    {
                        bulspd2 = Stats.Reload2;
                        ShootSecondCounter = 0;
                    }
                    else
                    {
                        bulspd2 = fireCD2;

                    }

                }
                if (stunningBulletspeed <= 0f && StunFirePoint==true)
                {
                   
                    StunsecondCounter++;
                    ShootStunFirePoint();

                    if (StunsecondCounter >= Stats.Stun_ammo_Capacity)
                    {
                        stunningBulletspeed = Stats.StunReload;
                        StunsecondCounter = 0;
                    }
                    else
                    {
                        stunningBulletspeed = fireCDStun;

                    }

                }

            }
        }
    }




    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(ProjectailPrefab, firePoint.position, firePoint.rotation);
        projectile bullet = bulletGO.GetComponent<projectile>();
       
        if (bullet != null)
        {
            Stats.Shoot(bullet, target, bularc);
            
        }
    }
    void ShootSecondFirePoint()
    {
        if (SecondFirePoint == true)
        {
            GameObject bulletGO2 = (GameObject)Instantiate(ProjectailPrefab2, firePoint2.position, firePoint2.rotation);
            projectile bullet2 = bulletGO2.GetComponent<projectile>();

            Stats.Shoot(bullet2, target, bularc2);
            if (bullet2 != null)
            {
                Stats.Shoot(bullet2, target, bularc2);

            }
        }
       
    }
    void ShootStunFirePoint()
    {
        if (StunFirePoint == true)
        {
            GameObject bulletGO3 = (GameObject)Instantiate(ProjectailStunPrefab, stunFirePoint.position, stunFirePoint.rotation);
            projectile stunningBullet = bulletGO3.GetComponent<StunningBullet>();
            Stats.ShootAndStun(stunningBullet, target, stunningBulletarc);

            if (stunningBullet != null)
            {
                Stats.ShootAndStun(stunningBullet, target, stunningBulletarc);
                

            }
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
