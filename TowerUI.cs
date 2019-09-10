using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour {
    //Build Manager Instance
    BuildManager buildManager;
    //Z position
    public Vector3 positionOffSet;

    // Use this for initialization
    void Start () {
        buildManager = BuildManager.instance;

    }

     void OnMouseDown()
    {
      //  buildManager.SelectNode(this);
    }

    //get position
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }
}
