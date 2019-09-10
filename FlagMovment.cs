using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagMovment : MonoBehaviour {
    FlagHandler flagHandler;
    Barracks barracks;
    public  bool FlagMode = false;
    public Barracks Clicked;

    private void Start()
    {
        flagHandler = GameObject.Find("BarracksSprite").GetComponent<FlagHandler>();
        barracks = GameObject.Find("BarracksSprite").GetComponent<Barracks>();
        gameObject.SetActive(FlagMode);
    }
    //private void Update()
    //{

    //    //if (flagHandler.gameObject.GetComponent<FlagHandler>().FlagVisible == true)
    //    if(flagHandler.FlagVisible == true)
    //    {
    //        gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

    public void Flagvisible()
    {
        gameObject.SetActive(true);
    }







   

}
