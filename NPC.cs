using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity{
    [Header("NPC")]
    public double Health = 100;
    public double Attack_distance = 1;
    public double Movement_speed = 1;
    [Range(0, 1)]
    public double Physical_defence;
    [Range(-1, 2)]
    public double Fire_defence;
    [Range(-1, 2)]
    public double Water_defence;
    [Range(-1, 2)]
    public double Air_defence;
    [Range(-1, 2)]
    public double Earth_defence;
    
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

    private GameObject Target;

    // Update is called once per frame
    void Update () {
		
	}
}
