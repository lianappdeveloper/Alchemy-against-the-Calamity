using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [Header("Helth and Defences")]
    public float StartHealth = 0;
    public float AttackRate = 0;
    [Range(0, 1)]
    public float StartDefenace = 0;
    [Range(0, 2)]
    public float StartFireRes = 0;
    [Range(0, 2)]
    public float StartWaterRes = 0;
    [Range(0, 2)]
    public float StartAirRes = 0;
    [Range(0, 2)]
    public float StartEarthRes = 0;
    public bool Flying;
    //[Header("Status Chances")]
    [System.Serializable]
    public struct Debuff
    {
        public double Timer;
        public double CurrentTimer;
        [Range(0, 1)]
        public float EffStr;
        public int Stack;
        [Range(0, 100)]
        public double Chance;
        public bool CurrentlyOn;
    }
    private Debuff Burns;
    private Coroutine F;
    private Debuff Frozen; //at effStr: 1=no effect, 0=complete standstill
    private Coroutine W;
    public Debuff Stunned;
    public Coroutine S;
    private Debuff Poisoned;
    delegate IEnumerator PoisoningMethod(float sick);
    List<Coroutine> Poisoning = new List<Coroutine>();
    private float defenace;
    private float FireRes;
    private float WaterRes;
    private float AirRes;
    private float EarthRes;

    public float MinPhysic;
    public float MaxPhysic;
    public float Fire;
    public float Water;
    public float Air;
    public float Earth;

    [Header("Game data")]
    public int Value = 0;
    public float distanceTravelled = 0;
    private float Health;
    public Image HealthBar;
    public GameObject BonePile;

    void Start()
    {
        Health = StartHealth;
        defenace = StartDefenace;
        FireRes = StartFireRes;
        WaterRes = StartWaterRes;
        AirRes = StartAirRes;
        EarthRes = StartEarthRes;
        Burns = SetDebuffs(PlayerStats._Burns);
        Frozen = SetDebuffs(PlayerStats._Frozen);
        Stunned = SetDebuffs(PlayerStats._Stunned);
        Poisoned = SetDebuffs(PlayerStats._Poisoned);
        FloatingTextController.Initialize();
    }

    private Debuff SetDebuffs(PlayerStats.Debuff player_debuff)
    {
        Debuff my_debuff = new Debuff();
        my_debuff.Timer = player_debuff.Timer;
        my_debuff.EffStr = player_debuff.EffStr;
        my_debuff.Stack = player_debuff.Stack;
        my_debuff.Chance = player_debuff.Chance;
        my_debuff.CurrentlyOn = false;
        return my_debuff;
    }

    public void CalcDamage(float dmg)
    {
        float PhysicHurt = (1 - defenace) * dmg;
        int display = Mathf.RoundToInt(PhysicHurt);
        FloatingTextController.CreateFloatingText(display.ToString(), transform);
        TakeDamage(PhysicHurt);
    }

    public void DealDamage(GameObject Target)
    {
        float Physic = Random.Range(MinPhysic, MaxPhysic);
        Target.GetComponent<AllyStats>().CalcDamage(Physic, Fire, Water, Air, Earth);

    }

    public void CalcDamage(float dmg, float fire, float water, float air, float earth)
    {
        float PhysicHurt = (1 - defenace) * dmg;
        float FireHurt = ((1 - FireRes) * fire);
        float WaterHurt = ((1 - WaterRes) * water);
        float AirHurt = ((1 - AirRes) * air);
        float EarthHurt = ((1 - EarthRes) * earth);

        if (FireHurt > 0)
        {
            int firedisp = Mathf.RoundToInt(FireHurt);
            FloatingTextController.CreateFloatingTextFire(firedisp.ToString(), transform);
            if (Random.Range(0F, 100F) <= Burns.Chance)
            {
                if (!Burns.CurrentlyOn)
                {

                    Burns.CurrentlyOn = true;
                    F = StartCoroutine(Burning(FireHurt * Burns.EffStr));
                }
                else
                {
                    StopCoroutine(F);
                    F = StartCoroutine(Burning(FireHurt * Burns.EffStr));
                }
            }
        }

        if (WaterHurt > 0)
        {
            int waterdisp = Mathf.RoundToInt(WaterHurt);
            FloatingTextController.CreateFloatingTextWater(waterdisp.ToString(), transform);
            if (Random.Range(0F, 100F) <= Frozen.Chance)
            {
                Frozen.CurrentTimer = Frozen.Timer;
                if (!Frozen.CurrentlyOn)
                {
                    Frozen.CurrentlyOn = true;
                    this.gameObject.GetComponent<EnemyPath>().SetSpeed(1 - Frozen.EffStr);
                    W = StartCoroutine(Freezing());
                }
                else
                {
                    StopCoroutine(W);
                    W = StartCoroutine(Freezing());
                }
            }
        }

        if (AirHurt > 0)
        {
            int Airdisp = Mathf.RoundToInt(AirHurt);
            FloatingTextController.CreateFloatingTextAir(Airdisp.ToString(), transform);
            if (Random.Range(0F, 100F) <= Stunned.Chance)
            {
                GetStunnedBitch();
            }
        }

        if (EarthHurt > 0)
        {
            int earthdisp = Mathf.RoundToInt(EarthHurt);
            FloatingTextController.CreateFloatingTextEarth(earthdisp.ToString(), transform);
            if (Random.Range(0F, 100F) <= Poisoned.Chance)
            {
                if (Poisoning.Count < Poisoned.Stack)
                {
                    Poisoning.Add(StartCoroutine(PoisonStackTimer(EarthHurt * Poisoned.EffStr)));

                }
                else
                {

                    StopCoroutine(Poisoning[0]);
                    Poisoning.RemoveAt(0);
                    Poisoning.TrimExcess();

                    Poisoning.Add(StartCoroutine(PoisonStackTimer(EarthHurt * Poisoned.EffStr)));
                }
            }
        }

        if (PhysicHurt > 0)
        {
            int display = Mathf.RoundToInt(PhysicHurt);
            FloatingTextController.CreateFloatingText(display.ToString(), transform);
        }

        TakeDamage(PhysicHurt + FireHurt + WaterHurt + AirHurt + EarthHurt);

    }

    public void TakeDamage(float amount) //the bullets call this function to hit the enemies
    {
        Health -= amount;

        HealthBar.fillAmount = Health / StartHealth; //fillAmount refers to health bar

        if (Health <= 0)
        {
            PlayerStats.Money += Value + Value * GameMaster.workCampAmount * 0.1f;
            Die();
        }
    }

    public void Die()
    {
        if (gameObject.name == "Skeleton")
        {
            
            Instantiate(BonePile, transform.position, transform.rotation);

        }

        Destroy(gameObject);
    }

    public void GetStunnedBitch()
    {
        Stunned.CurrentTimer = Stunned.Timer;
        if (!Stunned.CurrentlyOn)
        {
            Stunned.CurrentlyOn = true;
            this.gameObject.GetComponent<EnemyPath>().SetSpeed(0);
            S = StartCoroutine(Stunning());
        }
        else
        {
            StopCoroutine(S);
            S = StartCoroutine(Stunning());
        }
    }

    IEnumerator Burning(float flame)
    {
        for (int i = 0; i <= Burns.Timer; i++)
        {
            yield return new WaitForSeconds(1f);

            TakeDamage(flame);
            int firedisp = Mathf.RoundToInt(flame);
            FloatingTextController.CreateFloatingTextFire(firedisp.ToString(), transform);
        }
        Burns.CurrentlyOn = false;
        yield return null;
    }

    IEnumerator Freezing()
    {
        while (Frozen.CurrentlyOn)
        {
            if (Frozen.CurrentTimer <= 0)
            {
                this.gameObject.GetComponent<EnemyPath>().SetSpeed(1);
                Frozen.CurrentlyOn = false;
                yield return null;
            }
            else
                Frozen.CurrentTimer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator Stunning()
    {
        while (Stunned.CurrentlyOn)
        {
            if (Stunned.CurrentTimer <= 0)
            {
                this.gameObject.GetComponent<EnemyPath>().SetSpeed(1);
                Stunned.CurrentlyOn = false;
                yield return null;
            }
            else
                Stunned.CurrentTimer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator PoisonStackTimer(float sick)
    {
        for (int i = 0; i <= Poisoned.Timer; i++)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(sick);
            int earthdisp = Mathf.RoundToInt(sick);
            FloatingTextController.CreateFloatingTextEarth(earthdisp.ToString(), transform);
        }
        Poisoning.RemoveAt(0);
    }


}