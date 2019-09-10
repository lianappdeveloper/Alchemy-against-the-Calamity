using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reanimate : MonoBehaviour
{

    public float Reanimate_Time = 5f;
    public GameObject Skeleton;
    private float EnemyHealth;
    


    // Update is called once per frame
    void Update()
    {

        Reanimate_Time -= Time.deltaTime;

        if (Reanimate_Time<=0)
        {
            Instantiate(Skeleton, transform.position, transform.rotation);
            
            Destroy(this.gameObject);
        }






    }
}