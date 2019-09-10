using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimicalTrail : MonoBehaviour {
   
    public float AOEradius = 2f;
    public float Disapear;
    ChimicalThrower chimicalThrower;
    public float CD = 2f;
    private TowerStats Stats;
    private float Physic;
    private float Fire;
    private float Water;
    private float Air;
    private float Earth;


    public void Seek(float _Physic, float _Fire, float _Water, float _Air, float _Earth, float _AOEradius)
    { 
        Physic = _Physic;
        Fire = _Fire;
        Water = _Water;
        Air = _Air;
        Earth = _Earth;
        AOEradius = _AOEradius;
    }


    void Start()
    {
        Stats = this.gameObject.GetComponent<TowerStats>();
    }

    // Update is called once per frame
    void Update () {
        Disapear -= Time.deltaTime;
        onTouch();
        if (Disapear <= 0)
        {
                    Destroy(gameObject);
        }
	}

   
    private void onTouch()
    {
        CD -= Time.deltaTime;

        //finds everything he collide with
        Collider[] colliders = Physics.OverlapSphere(transform.position, AOEradius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                if (CD <= 0f)
                {
                    Damage(collider.transform);
                    CD = 1.5f;
                }
            }
        }

    }

    //dmg claclution
    void Damage(Transform Enemy)
    {
        
            Enemy.GetComponent<EnemyStats>().CalcDamage(Physic, Fire, Water, Air, Earth);
            Stats.Shoot(Enemy, 0.5f);

    }
}
