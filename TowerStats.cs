using System.Collections;
using UnityEngine;

public class TowerStats : MonoBehaviour {

    [Header("Range of the physical damage")]
    public float MinDmg;
    public float MaxDmg;
    public int ammo_Capacity = 1;
    public int ammo_Capacity2 = 1;
    public int Stun_ammo_Capacity = 1;
    public float Reload = 3;
    public float Reload2 = 3;
    public float StunReload = 3;
    EnemyStats Enemy;
    [Header("Conversion to chemical", order = 0)]
    [Space(-10, order = 1)]
    [Header("(put 0.1 for 10% for example)", order = 2)]
    public float FireParcent;
    public float WaterParcent;
    public float AirParcent;
    public float EarthParcent;
    [Header("If = 0, attacks a single target", order = 3)]
    [Space(-10, order = 4)]
    [Header("If > 0, attacks deal AOE damage", order = 5)]
    public float AOEradius;
    private float Physic;
    private float Fire;
    private float Water;
    private float Air;
    private float Earth;
    [Header("Upgrade")]
    public Transform Next_Upgrade;
    public int cost;
    [Header("Tower Cost")]
    public int TowerCost;

    
    //Build Manager Instance
    BuildManager buildManager;
    //Z position
    public Vector3 positionOffSet;
    // Use this for initialization
    void Start()
    {
        buildManager = BuildManager.instance;
     

    }
    void OnMouseDown()
    {
        buildManager.SelectNode(this);
    }
    //get position
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }


    public void Shoot(projectile bullet, Transform target, AnimationCurve arc)
    {
        Physic = Random.Range(MinDmg, MaxDmg);
        Fire = Physic * FireParcent;
        Water = Physic * WaterParcent;
        Air = Physic * AirParcent;
        Earth = Physic * EarthParcent;
        Enemy = target.gameObject.GetComponent<EnemyStats>();
        bullet.Seek(target, Physic, Fire, Water, Air, Earth, AOEradius, arc);
    }

    public void Shoot(ChimicalTrail ChemTrail)
    {
        Physic = Random.Range(MinDmg, MaxDmg);
        Fire = Physic * FireParcent;
        Water = Physic * WaterParcent;
        Air = Physic * AirParcent;
        Earth = Physic * EarthParcent;
        ChemTrail.Seek(Physic, Fire, Water, Air, Earth, AOEradius);
    }

    public void Shoot(Transform target, float Multplier)
    {
        Physic = Random.Range(MinDmg, MaxDmg);
        Fire = Physic * FireParcent;
        Water = Physic * WaterParcent;
        Air = Physic * AirParcent;
        Earth = Physic * EarthParcent;
        Enemy = target.gameObject.GetComponent<EnemyStats>();
        Enemy.CalcDamage((Physic + Fire + Water + Air + Earth) * Multplier);
    }

    public void ShootAndStun(projectile bullet, Transform target, AnimationCurve arc)
    {
        Physic = Random.Range(MinDmg, MaxDmg);
        Fire = 0;
        Water = 0;
        Air = 0;
        Earth = 0;
        bullet.Seek(target, Physic, Fire, Water, Air, Earth, AOEradius, arc);
    }

    
}
