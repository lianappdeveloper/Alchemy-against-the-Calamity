using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall: MonoBehaviour
{
    [Header("stats")]
    public float range = 0.1f;
    //public float StopRange = 0.1f;
    public bool CanTargetFlying;
    [Header("fields")]
    //private Transform Target;
    //private GameObject TargetEnemy = null;
    private static string EnemyTag = "Enemy";
    //private float ClosestTarget = 0;
    
    public float Defenace = 10;
    public float FireRes = 0;
    public float WaterRes = 0;
    public float AirRes = 0;
    public float EarthRes = 0;
    public float Health = 500f;

    void Start()
    {
        this.gameObject.GetComponent<AllyStats>().SetStats(Health, Defenace, FireRes, WaterRes, AirRes, EarthRes);
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        foreach (GameObject Enemy in enemies)
        {
            bool IsEnemyFlying = Enemy.GetComponent<EnemyStats>().Flying;
            float distanceToEnemy = Vector2.Distance(this.gameObject.transform.position, Enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                if (IsEnemyFlying == true && CanTargetFlying == false)
                {
                }
                else Enemy.GetComponent<EnemyPath>().Nemesis = this.gameObject;
            }
        }
    }
}