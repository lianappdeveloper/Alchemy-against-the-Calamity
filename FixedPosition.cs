using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPosition : MonoBehaviour {
    //if position as been edit do +1
    public int PositionChanged;
    

    // Use this for initialization
    void Start () {
        PositionChanged = 0;
        Vector3 position = transform.position;
        position.z = position.z - 2;
        position.y = position.y + 2;
        transform.position = position;
     
    }
	

}
