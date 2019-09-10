using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnit : MonoBehaviour
{
   [Header("stats")]
    public float speed = 10f;
    public float range = 3f;
    public float StopRange = 0.1f;
    public bool CanTargetFlying;
    [Header("fields")]
    private Transform Target;
    private Transform Home;
    public Barracks Daddy;
    public int wavePointIndex;
    private float AttackRate;
    private float CountDown;
    private GameObject TargetEnemy = null;
    private static string EnemyTag = "Enemy";
    private float ClosestTarget = 0;

    void Start()
    {
        Home = Daddy.spawnPoint.Dpoints[wavePointIndex];
        AttackRate = Daddy.AttackRate;
        CountDown = AttackRate;
        Target = Home;
    }

    void Update()
    {
        CountDown -= Time.deltaTime;
        if (Target == Home) //search for an enemy only when you don't have one
        {
            TargetEnemy = null;
            UpdateTarget();
        }

        if (Target == null) //if the enemy has died, go back to home
        {
            Target = Home;
            StopRange = 0.1f;
        }
        else
        {
            if (Target != Home && TargetEnemy != null)
            {
                if (TargetEnemy.GetComponent<EnemyPath>().Nemesis == null)
                    TargetEnemy.GetComponent<EnemyPath>().Nemesis = this.gameObject;
                else if (TargetEnemy.GetComponent<EnemyPath>().Nemesis != this.gameObject)
                    UpdateTarget();
            }
            if (Vector2.Distance(transform.position, Target.position) > StopRange) //move closer to the target
            {

                Vector2 dir = Target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }
            else if (Target != Home && TargetEnemy !=null) //an enemy is being targeted, and is within attacking range
            {
                if (CountDown <= 0)
                {
                    this.gameObject.GetComponent<AllyStats>().DealDamage(TargetEnemy);
                    CountDown = AttackRate;
                }
            }
        }
    }

    void UpdateTarget()
    {
        ClosestTarget = range;
       
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        foreach(GameObject Enemy in enemies)
        {
            bool IsEnemyFlying = Enemy.GetComponent<EnemyStats>().Flying;
            float distanceToEnemy = Vector2.Distance(Home.position, Enemy.transform.position);
            if(distanceToEnemy<= range && distanceToEnemy< ClosestTarget)
            {
                if (IsEnemyFlying == true && CanTargetFlying == false)
                {
                }
                else
                {
                    distanceToEnemy = ClosestTarget;
                    TargetEnemy = Enemy;
                }
            }
        }
        if (TargetEnemy != null)
        {
            Target = TargetEnemy.transform;
            StopRange = 1;
        }
    }
}