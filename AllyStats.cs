using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyStats : MonoBehaviour {
    [Header("Helth and Defences")]

    private float Defenace = 10;
    private float FireRes = 0;
    private float WaterRes = 0;
    private float AirRes = 0;
    private float EarthRes = 0;
    private float StartHealth = 500f;
    private float MinPhysic;
    private float MaxPhysic;
    private float Fire;
    private float Water;
    private float Air;
    private float Earth;
    
    //private float ResTimer;
    [Header("Game data")]
    //private GameObject EnemysTarget;
    public int Value = 0;
    private float Health;
    public Image HealthBar;
    //Vector2 lastPosition;
    public Barracks Daddy;
    public int wavePointIndex;

    void Start()
    {
        Health = StartHealth;
    }

    public void SetStats(float _Health, float _MinPhysic, float _MaxPhysic, float _Fire, float _Waters, float _Air, float _Earth, float _Defenace, float _FireRes, float _WaterRes, float _AirRes, float _EarthRes)
    {
        StartHealth = _Health;
        MinPhysic = _MinPhysic;
        MaxPhysic = _MaxPhysic;
        Fire = _Fire;
        Water = _Waters;
        Air = _Air;
        Earth = _Earth;
        Defenace = _Defenace;
        FireRes = _FireRes;
        WaterRes = _WaterRes;
        AirRes = _AirRes;
        EarthRes = _EarthRes;
    }

    public void SetStats(float _Health, float _Defenace, float _FireRes, float _WaterRes, float _AirRes, float _EarthRes)
    {
        StartHealth = _Health;
        Defenace = _Defenace;
        FireRes = _FireRes;
        WaterRes = _WaterRes;
        AirRes = _AirRes;
        EarthRes = _EarthRes;
        MinPhysic = 0;
        MaxPhysic = 0;
        Fire = 0;
        Water = 0;
        Air = 0;
        Earth = 0;
    }

    public void CalcDamage(float dmg, float fire, float water, float air, float earth) //the bullets call this function to hit the enemies
    {
        float PhysicHurt = (1 - Defenace) * dmg;
        float FireHurt = ((1 - FireRes) * fire);
        float WaterHurt = ((1 - WaterRes) * water);
        float AirHurt = ((1 - AirRes) * air);
        float EarthHurt = ((1 - EarthRes) * earth);
        TakeDamage(PhysicHurt + FireHurt + WaterHurt + AirHurt + EarthHurt);
    }

    public void TakeDamage(float amount) 
    {
        int display = Mathf.RoundToInt(amount);
        FloatingTextController.CreateFloatingText(display.ToString(), transform);

        Health -= amount;
        
        HealthBar.fillAmount = Health / StartHealth; //fillAmount refers to health bar

        if (Health <= 0)
        {
           
            Die();
        }
    }

    public void DealDamage(GameObject Target)
    {
        float Physic = Random.Range(MinPhysic, MaxPhysic);
        Target.GetComponent<EnemyStats>().CalcDamage(Physic, Fire, Water, Air, Earth);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

