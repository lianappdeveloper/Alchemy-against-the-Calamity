using System.Collections;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private ChimicalTrail ChemTrailScript;
    public bool spawnChimical = false;
    public GameObject ChemTrail;
    private AnimationCurve Arc;
    private Coroutine Coroutine;
    protected Transform target;
    private Vector3 Start1;
    public float speed = 0.4f;
    private float Physic;
    private float Fire;
    private float Water;
    private float Air;
    private float Earth;
    private float AOEradius = 0f;

    public void Seek(Transform _target,float _Physic, float _Fire, float _Water, float _Air, float _Earth, float _AOEradius, AnimationCurve _Ark)
    {
        target = _target;
        Arc = _Ark;
        Start1 = transform.position;
        Physic = _Physic;
        Fire = _Fire;
        Water = _Water;
        Air = _Air;
        Earth = _Earth;
        AOEradius = _AOEradius;
        Coroutine = StartCoroutine(Curve());
    }

    void Start()
    {
        ChemTrailScript = ChemTrail.GetComponent<ChimicalTrail>();
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator Curve()
    {
        float time = 0f;
        Transform end = target;
        while (time < speed)
        {
            if (target == null)
            {
                Destroy(gameObject);
                break;
            }

            time += Time.deltaTime;

            float linearT = time / speed;
            float heightT = Arc.Evaluate(linearT);
            float height = Mathf.Lerp(0f, 3.0f, heightT); // change 3 to however tall you want the arc to be
            transform.position = Vector3.Lerp(Start1, target.position, linearT) + new Vector3(0f, height);
            transform.rotation = Quaternion.LookRotation(transform.position);
            yield return null;
        }

        // impact
        HitTarget();
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
            ChimicalSpawn();
            Destroy(gameObject);
        }
    }


    public void ChimicalSpawn()
    {
        if (spawnChimical == true)
        {
            GameObject spawn = Instantiate(ChemTrail, target.transform.position, target.transform.rotation);
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
        if (Enemy != null)
            Enemy.GetComponent<EnemyStats>().CalcDamage(Physic, Fire, Water, Air, Earth);
    }
}
