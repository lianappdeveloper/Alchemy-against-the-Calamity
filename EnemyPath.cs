
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    public float Speed = 10f;
    private float CountDown;
    private float CurrentSpeed;
    public Transform Target;
    private int wavePointIndex = 0;
    public float distanceTravelled = 0;
    public static string Allytag = "Ally";
    public GameObject TargetAlly = null;
    [Header ("CombatStats")]
    public float range = 3;
    public float CombatRange = 1f;
    public float AttSpd = 0f;
    public float AttCD;
    public float AttRate = 2f;
    public float StopRange = 1;
    [Header("Skills")]
    public Transform Skill;
    public float skill_CD = 10f;
    public Transform skill2;
    public float skill2_CD = 5f;
   


    public GameObject Nemesis;
    private EnemyStats MyStats;

    Vector2 lastPosition;
    
    void Start()
    {
        MyStats = this.gameObject.GetComponent<EnemyStats>();
        CountDown = MyStats.AttackRate;
        CurrentSpeed = Speed;
        lastPosition = transform.position;
        Target = waypoint.points[0];
        
    }

    void Update()
    {
        skill_CD -= Time.deltaTime;
        if (skill_CD <= 0 && Nemesis != null)
        {
            //Perform skill


        }
        skill2_CD -= Time.deltaTime;
        if (skill2_CD <= 0 && Nemesis != null)
        {
            //Perform skill 2

        }
       

        CountDown -= Time.deltaTime;
        if (Nemesis == null || Vector2.Distance(transform.position, Nemesis.transform.position) > StopRange)
            KeepGoing();
        else if (CountDown <= 0 && Vector2.Distance(transform.position, Nemesis.transform.position) <= StopRange)
        {
            CountDown = MyStats.AttackRate;
            MyStats.DealDamage(Nemesis);
        }
    }

    public void SetSpeed(float SlowEffect)
    {
        CurrentSpeed = Speed * SlowEffect;
    }

    void KeepGoing()
    {
        //distance travled calc
        distanceTravelled += Vector2.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        Vector2 dir = Target.position - transform.position;
        transform.Translate(dir.normalized * CurrentSpeed * Time.deltaTime, Space.World);
        if (Vector2.Distance(transform.position, Target.position) <= 0.5f)
        {

            Vector2 Despawner = transform.position;
            GetNextWaypoint();
        }
    }
    
    void GetNextWaypoint()
    {
        //Destroy upon reching the end of the road
        if (wavePointIndex >= waypoint.points.Length - 1)
        {
            GameMaster.PlayerHealth--;
            Destroy(gameObject);
            return;
        }

        wavePointIndex++;
        Target = waypoint.points[wavePointIndex];
    }
}
