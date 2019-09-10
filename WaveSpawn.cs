using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawn : MonoBehaviour {

    public enum SpawnState { Spawning, Waiting, Counting };
    public enum WhatEnemyIsSpawning { First, Second , Third,FINISH };



    [System.Serializable]
    //wave attributes
    public class Wave {

        public string name;
        public Transform enemy;
        public int FirstEnemyCount;
        public Transform enemy1;
        public int SecondEnemyCount;
        public Transform enemy2;
        public int ThirdEnemyCount;
        /*  public Transform enemy3;
          public Transform enemy4;
          public Transform enemy5;
          public Transform enemy6;
          */
        public int count;
        public float rate;
        
            
    }
    public Wave[] Waves;
    //Types of enemies
    public string EnemyTag = "Enemy";
    
  
 
    
    //Game attribuets
    public Text waveCounDownText;
    public Transform spawnPoint;
    private int nextWave = 0;
    public float timeBetweenWaves = 15f;
    private float waveCountDown;
    bool isWaveCompleted = false;
   




    private SpawnState State = SpawnState.Counting;
    private WhatEnemyIsSpawning EnemyType = WhatEnemyIsSpawning.First;
    

    //wait 15 sec between each wave
    void Start()
    {
        EnemyType = WhatEnemyIsSpawning.First;
        waveCountDown = timeBetweenWaves;
        
   

    }

  
    //checks if were currently spawning and if not start spawn
     void Update()
    {
        // if u want countdown evey sec
       //waveCountDown -= Time.deltaTime;
      

        //checks if enemies are still alive
        if (!enemyIsAlive())
        {
            
               waveCompleted();
        
        }


        if (waveCountDown <= 0)
        {


            if (State != SpawnState.Spawning)
            {
                
                NextWave();
            }

            else
            {
                waveCountDown = timeBetweenWaves;


            }
        }
    }
    
   

   public void waveCompleted()
    {

        State = SpawnState.Counting;

        NextWave();

    }
   public void NextWave()
    {
     
        if (nextWave  >= Waves.Length)
        {
        }
        else
        {
            StartCoroutine(SpawnEnemy(Waves[nextWave]));
            nextWave++;
        }
    }

    bool enemyIsAlive()
    {
        if (GameObject.FindGameObjectsWithTag(EnemyTag) == null)
        {
          
            return false;
      
        }


       else

        waveCountDown -= Time.deltaTime;
        waveCounDownText.text = Mathf.Round(waveCountDown).ToString();

        if (State == SpawnState.Spawning)
        {
            waveCountDown = timeBetweenWaves;
        }
       
        return true;

    }
        

    
    
    /* f= First
       s= Second
       t=Third
*/
    //spawn enemy
    IEnumerator SpawnEnemy(Wave _wave)
    {
        EnemyType = WhatEnemyIsSpawning.First;
      

        State = SpawnState.Spawning;

        //spawn FIRST enemy
        if (EnemyType == WhatEnemyIsSpawning.First)
        {
            for (int f = 0; f < _wave.FirstEnemyCount; f++)
            {
                EnemyType = WhatEnemyIsSpawning.First;
                SpawnEnemy(_wave.enemy);
                yield return EnemyType = WhatEnemyIsSpawning.Second;
                yield return new WaitForSeconds(1f / _wave.rate);
            }
        }
       

      

        //spawn SECOND enemy
        if (EnemyType == WhatEnemyIsSpawning.Second)
        {

           

            for (int s = 0; s < _wave.SecondEnemyCount; s++)

            {
                SpawnEnemy1(_wave.enemy1);
                yield return EnemyType = WhatEnemyIsSpawning.Third;
                yield return new WaitForSeconds(3.5f / _wave.rate);

            }
        }

        //spawn THIRD enemy
        if (EnemyType == WhatEnemyIsSpawning.Third)
        {

            for (int t = 0; t < _wave.ThirdEnemyCount; t++)
            {
                Debug.Log("spawn the THIRD enemy");
                SpawnEnemy2(_wave.enemy2);
                yield return EnemyType = WhatEnemyIsSpawning.FINISH;
                yield return new WaitForSeconds(6.2f / _wave.rate);

            }
        }
 

        /*    SpawnEnemy(_wave.enemy3);
            SpawnEnemy(_wave.enemy4);
            SpawnEnemy(_wave.enemy5);
            SpawnEnemy(_wave.enemy6);
*/

        // time between every enemy spawn (spawn rate)
        //yield return new WaitForSeconds(1f / _wave.rate);





        State = SpawnState.Waiting;

        EnemyType = WhatEnemyIsSpawning.FINISH;

     

        yield break;



    }
    //spawn the First enemy
    void SpawnEnemy(Transform _enemy)
    {
      

        //spawn point
        Instantiate(_enemy, spawnPoint.position, spawnPoint.rotation);

        

    }
    //spawn the Second enemy
    void SpawnEnemy1(Transform _enemy1)
    {


        //spawn point
        Instantiate(_enemy1, spawnPoint.position, spawnPoint.rotation);


    }
    //spawn the Third enemy
    void SpawnEnemy2(Transform _enemy2)
    {


        //spawn point
        Instantiate(_enemy2, spawnPoint.position, spawnPoint.rotation);


    }
 

}
  


