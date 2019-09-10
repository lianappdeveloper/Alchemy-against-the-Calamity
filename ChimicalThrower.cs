using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimicalThrower : MonoBehaviour {

    [Header("stats")]
    public float range = 10f;
    //public float DMGOverTime;
    [Header("fields")]
    private Transform target;
    public Transform firePoint;
    public static string EnemyTag = "Enemy";
    public GameObject Enemy;
    public float ClosestEnemy;
    public float ClosestTarget = 0;
    private GameObject TargetEnemy;
    public GameObject trailEffect;
    public float CD = 2f;
    private TowerStats Stats;


    [Header("chimicals")]
    public bool useChimical = false;
    public LineRenderer lineRenderer;

    public GameObject ChemTrail;
    private ChimicalTrail ChemTrailScript;


    void Start()
    {
        Stats = this.gameObject.GetComponent<TowerStats>();
        ChemTrailScript = ChemTrail.GetComponent<ChimicalTrail>();
        InvokeRepeating("UpdateTarget", 0f, 0.1f);
        //ChemTrailScript.OverTimeDMG = DMGOverTime / 2f;
    }


    void UpdateTarget()
    {
      
        float ClosestTarget = 0f;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        foreach (GameObject Enemy in enemies)
        {
            EnemyPath E_Path = Enemy.GetComponent<EnemyPath>();
            float distanceToEnemy = Vector2.Distance(transform.position, Enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                ClosestEnemy = E_Path.distanceTravelled;
                if (ClosestEnemy > ClosestTarget)
                {
                    ClosestTarget = ClosestEnemy;
                    TargetEnemy = Enemy;
                    target = TargetEnemy.transform;
                }
            }
        }
        if (TargetEnemy != null)
        {
            if (Vector2.Distance(transform.position, TargetEnemy.transform.position) > range)
            {
                TargetEnemy = null;
                target = null;
                ClosestTarget = 0;
            }
            else
            {
                if (useChimical)
                {
                    Chimical();
                    GameObject spawn =  Instantiate(ChemTrail, TargetEnemy.transform.position, TargetEnemy.transform.rotation);
                    Stats.Shoot(spawn.GetComponent< ChimicalTrail>());
                }
            }
           
        }
    }
    //define the end and begining of the projectile line
    void Chimical()
    {
        Stats.Shoot(target, 1);
        //target.GetComponent<EnemyStats>().TakeDamage(DMGOverTime );

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        if (!lineRenderer.enabled)
        {
            //will define the trail effect start and end on the right target
            lineRenderer.enabled = true;
            

        }
        
    }

     private void Update()
     {

        if (TargetEnemy == null)
        {
            if (useChimical)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    //trailEffect.enabled = false;

                }
            }
            return;
        }
     }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
