using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workamp : MonoBehaviour {



      private EnemyStats enemyStats;
      private Workamp workCamp;
  



    public int MPS;

     void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    // Use this for initialization
    void Start () {
        GameMaster.workCampAmount++;




    
    }



	// Update is called once per frame
	void Update () {

       // PlayerStats.Money += MPS * Time.deltaTime;
      
        
    }
}
