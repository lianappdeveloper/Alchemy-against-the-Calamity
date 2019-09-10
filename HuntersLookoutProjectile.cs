using System.Collections.Generic;
using UnityEngine;

public class HuntersLookoutProjectile : MonoBehaviour
{

    private Transform target;
    public float speed = 10f;
    public float Physic;
    public float Fire;
    public float Water;
    public float Air;
    public float Earth;
    public float AOEradius = 0f;

    public void Seek(Transform _target, float _Physic, float _Fire, float _Water, float _Air, float _Earth, float _AOEradius)
    {
        target = _target;
        Physic = _Physic;
        Fire = _Fire;
        Water = _Water;
        Air = _Air;
        Earth = _Earth;
        AOEradius = _AOEradius;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;

        }
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float disThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= disThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * disThisFrame, Space.World);

    }

    void HitTarget()
    {
        //aoe if hits and if not kills damage the rest
        if (AOEradius > 0)
        {
            Explosion();
        }
        else
        {
            Damage(target);
            Destroy(gameObject);
        }
    }

    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AOEradius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
        Destroy(gameObject);
    }

    void Damage(Transform Enemy)
    {
        Enemy.GetComponent<EnemyStats>().CalcDamage(Physic, Fire, Water, Air, Earth);
    }
}
