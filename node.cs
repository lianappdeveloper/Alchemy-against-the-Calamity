﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node  {


    public bool walkable;
    public Vector3 WorldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public node parent;
    //constractor
    public node(bool _walkable, Vector3 _worldpos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        WorldPosition = _worldpos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost() {
       return  gCost + hCost;
      
    }
}
