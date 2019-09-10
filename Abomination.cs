using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abomination : MonoBehaviour
{
    //private float ResTimer;

    [Header("Helth and Defences")]
    public float Defenace = 10;
    public float FireRes = 0;
    public float WaterRes = 0;
    public float AirRes = 0;
    public float EarthRes = 0;
    public float StartHealth = 500f;
    public float MinPhysic;
    public float MaxPhysic;
    public float Fire;
    public float Water;
    public float Air;
    public float Earth;

    [Header("stats")]
    public float speed = 5f;
    public float range = 3f;
    public float StopRange = 0.1f;
    public bool CanTargetFlying;
    public bool AbominationIsTargeted = false;
    private EnemyPath EnemyTarget;


    [Header("fields")]
    private Transform Target;
    private float AttackRate;
    public float CountDown;
    private GameObject TargetEnemy = null;
    private static string EnemyTag = "Enemy";
    private float ClosestTarget = 0;
    public GameObject AbominationPoint;
    private Vector3 WalkingTarget;

    [Header("Skills")]
    public Transform Skill;
    public float skill_CD=10f;
    public Transform skill2;
    public float skill2_CD=5f;
    public Transform skill3;
    public float skill3_CD=7f;


    void Start()
    {
        
        CountDown = AttackRate;
        this.gameObject.GetComponent<AllyStats>().SetStats(StartHealth, MinPhysic, MaxPhysic, Fire, Water, Air, Earth, Defenace, FireRes, WaterRes, AirRes, EarthRes);

    }

    void Update()
    {
        skill_CD -= Time.deltaTime;
        if (skill_CD <=0 && TargetEnemy!= null)
        {
            //Perform skill


        }
        skill2_CD -= Time.deltaTime;
        if (skill2_CD <= 0 && TargetEnemy != null)
        {
            //Perform skill 2

        }
        skill3_CD -= Time.deltaTime;
        if (skill3_CD <= 0)
        {
            //Perform skill 3
        }

        if (Input.GetMouseButtonDown(1))
        {
            AbominationIsTargeted = true;
            Instantiate(AbominationPoint);

        }
        if (Input.GetMouseButtonDown(0))
        {

            AbominationIsTargeted = false;

        }

        CountDown -= Time.deltaTime;

        if (AbominationIsTargeted == true) //if the user right clicked the abomination will move to the mouse position
        {
            EnemyTarget = this.gameObject.GetComponent<EnemyPath>();
            TargetEnemy = null;
            WalkingTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            WalkingTarget.z = -1;
            transform.position = Vector3.MoveTowards(transform.position, WalkingTarget, speed * Time.deltaTime);

            if (EnemyTarget.Nemesis == null)
            {
                TargetEnemy = null;
                WalkingTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                WalkingTarget.z = -1;
                transform.position = Vector3.MoveTowards(transform.position, WalkingTarget, speed * Time.deltaTime);
            }
          
             
            
        }
        else
        {

            UpdateTarget();
        }

        if (Target == null) //if the enemy has died, go back to home
        {

            StopRange = 0.1f;
            CancelInvoke("WeFIght");
        }
        else
        {


            if (Target != null && TargetEnemy != null)
            {
                if (TargetEnemy.GetComponent<EnemyPath>().Nemesis == null)
                {

                    TargetEnemy.GetComponent<EnemyPath>().Nemesis = this.gameObject;
                }
                else if (TargetEnemy.GetComponent<EnemyPath>().Nemesis != this.gameObject)
                    UpdateTarget();
            }
            if (Vector2.Distance(transform.position, Target.position) > StopRange) //move closer to the target
            {
                Vector2 dir = Target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }
            else if (Target != null && TargetEnemy != null) //an enemy is being targeted, and is within attacking range
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
        foreach (GameObject Enemy in enemies)
        {
            bool IsEnemyFlying = Enemy.GetComponent<EnemyStats>().Flying;
            float distanceToEnemy = Vector2.Distance(this.transform.position, Enemy.transform.position);
            if (distanceToEnemy <= range && distanceToEnemy < ClosestTarget)
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
            StopRange = 1f;
        }
    }
}
