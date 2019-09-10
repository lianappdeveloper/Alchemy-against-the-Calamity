using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    
    public Text MoneyTXT;

    [System.Serializable]
    public struct Debuff
    {
        public double Timer;
        [Range(0, 1)]
        public float EffStr;
        public int Stack;
        [Range(0, 100)]
        public double Chance;
    }
    public Debuff Burns;
    public Debuff Frozen;
    public Debuff Stunned;
    public Debuff Poisoned;

    public static Debuff _Burns;
    public static Debuff _Frozen;
    public static Debuff _Stunned;
    public static Debuff _Poisoned;

    public static float Money;
    public static int StartMoney = 200;
    private int moneyDis;

    private void Start()
    {
        Money = StartMoney;
        _Burns = Burns;
        _Frozen = Frozen;
        _Stunned = Stunned;
        _Poisoned = Poisoned;

        MoneyTXT.text = StartMoney.ToString();
    }
    private void Update()
    {
        moneyDis = Mathf.RoundToInt(Money);
        MoneyTXT.text = moneyDis.ToString();
    }
}
