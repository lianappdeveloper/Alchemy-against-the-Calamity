using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHandler : MonoBehaviour {

    public bool FlagVisible;
    public Barracks barracks;
    public GameObject dp;


    void Start () {
        barracks = gameObject.GetComponent<Barracks>();
      
       
       
    }
    private void OnMouseDown()
    {
        
        if (FlagVisible == true)    
        {
            
            dp.gameObject.SetActive(true);
            FlagVisible = false;
          
        }
        else
        {
            dp.gameObject.SetActive(false);
            FlagVisible = true;
        }
   
    }

}
