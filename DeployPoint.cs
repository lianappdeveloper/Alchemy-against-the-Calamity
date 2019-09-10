using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployPoint : MonoBehaviour
{
    
    public  Transform[] Dpoints;


    private void Awake()
    {
        Dpoints = new Transform[transform.childCount];


        for (int i = 0; i < Dpoints.Length; i++)
        {

            Dpoints[i] = transform.GetChild(i);
        }
    }
}


