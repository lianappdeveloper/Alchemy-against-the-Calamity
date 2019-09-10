using System.Collections;
using UnityEngine;

public class projectileAOE : MonoBehaviour
{
    public AnimationCurve Arc;
    private Coroutine Coroutine;
    protected Transform target;
    private Vector3 Start;
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
        Start = transform.position;
        Physic = _Physic;
        Fire = _Fire;
        Water = _Water;
        Air = _Air;
        Earth = _Earth;
        AOEradius = _AOEradius;
    }

    void Update()
    {
        //if (target == null)
        //{
        //    Destroy(gameObject);
        //    return;

        //}
        //Vector3 dir = target.position - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //float disThisFrame = speed * Time.deltaTime;
        //if (dir.magnitude <= disThisFrame)
        //{
        //    HitTarget();
        //    return;
        //}
        //transform.Translate(dir.normalized * disThisFrame, Space.World);
        if (Coroutine == null)
        {
            Coroutine = StartCoroutine(Curve());
        }
    }

    IEnumerator Curve()
    {
        float duration = 0.40f;
        float time = 0f;

        Vector3 end = target.position - (target.forward * 0.55f); // lead the target a bit to account for travel time, your math will vary

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = Arc.Evaluate(linearT);

            float height = Mathf.Lerp(0f, 3.0f, heightT); // change 3 to however tall you want the arc to be

            transform.position = Vector2.Lerp(Start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }

        // impact

        Coroutine = null;
    }

    public virtual void HitTarget()
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

    protected void Damage(Transform Enemy)
    {
        Enemy.GetComponent<EnemyStats>().CalcDamage(Physic, Fire, Water, Air, Earth);
    }
}
