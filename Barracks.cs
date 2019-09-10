//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{

    
    [Header("stats")]
    public float range = 10f;
    public Vector3 rangeV;
    public float spawnRate = 2f;
    public int Capasety = 3;

    [Header("fields")]

    public GameObject AllyPrefab;
    public Transform Entrance;
    public DeployPoint spawnPoint;
    public static string AllyTag = "Ally";
    private GameObject[] Ally;
    public float[] AllySpawnCD;

    [Header("Spawned units stats")]
    [Header("")]
    public float StartHealth = 500f;
    public float AttackRate = 10f; //attack every x sec
    [Header("Range of the physical damage")]
    public float MinPhysic;
    public float MaxPhysic;
    [Header("Conversion to chemical", order = 0)]
    [Space(-10, order = 1)]
    [Header("(put 0.1 for 10% for example)", order = 2)]
    public float FireParcent;
    public float WaterParcent;
    public float AirParcent;
    public float EarthParcent;
    private float Physic;
    private float Fire;
    private float Water;
    private float Air;
    private float Earth;

    [Header("Defeces")]
    public float Defenace = 10;
    public float FireRes = 0;
    public float WaterRes = 0;
    public float AirRes = 0;
    public float PoisRes = 0;

    private void Start()
    {
        rangeV.x = 0;
        rangeV.y = 0;
        rangeV.z = -1;
        Physic = Random.Range(MinPhysic, MaxPhysic);
        Fire = Physic * FireParcent;
        Water = Physic * WaterParcent;
        Air = Physic * AirParcent;
        Earth = Physic * EarthParcent;
        AllySpawnCD = new float[Capasety];
        Ally = new GameObject [Capasety];
        for (int i = 0; i < Capasety; i++)
        {
            SpawnAlly(i);
        }
    }

    private void Update()
    {
        for(int i=0;i<Capasety;i++)
        {
            if (Ally[i] == null)
                AllySpawnCD[i] -= Time.deltaTime;
            if (AllySpawnCD[i]<=0)
            {
                SpawnAlly(i);
            }
        }
    }





    public void SpawnAlly(int SoldierIndex)
    {
        Ally[SoldierIndex] = Instantiate(AllyPrefab, Entrance.position, Entrance.rotation);
        Ally[SoldierIndex].GetComponent<AllyUnit>().wavePointIndex = SoldierIndex;
        Ally[SoldierIndex].GetComponent<AllyUnit>().Daddy = this;
        Ally[SoldierIndex].GetComponent<AllyStats>().Daddy = this;
        Ally[SoldierIndex].GetComponent<AllyStats>().wavePointIndex = SoldierIndex;
        Ally[SoldierIndex].GetComponent<AllyStats>().SetStats(StartHealth, MinPhysic, MaxPhysic, Fire, Water, Air, Earth, Defenace, FireRes, WaterRes, AirRes, PoisRes);
        AllySpawnCD[SoldierIndex] = spawnRate;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}