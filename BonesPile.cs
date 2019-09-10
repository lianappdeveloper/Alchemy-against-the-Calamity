using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F : MonoBehaviour {
   
    // Use this for initialization
    void Start () {
        GameObject Enemy = GameObject.Find("Enemy");
        EnemyStats enemyStats = Enemy.GetComponent<EnemyStats>();
        enemyStats.StartHealth= enemyStats.StartHealth / 2f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
