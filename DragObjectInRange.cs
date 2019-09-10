using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectInRange : MonoBehaviour {

    private Vector3 Offset;
    Barracks barracks;
    // Use this for initialization
    void Start () {
        barracks = GameObject.Find("BarracksSprite").GetComponent<Barracks>();


    }
	
	// Update is called once per frame
	void Update () {

       

    }
    void OnMouseDown()
    {

        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7);
        Offset = transform.position - Camera.main.ScreenToWorldPoint(mousePos);
    }
     void OnMouseDrag()
    {

        float radius = barracks.range; ; //radius
        Vector3 centerPosition = barracks.rangeV; //center of the range
        float distance = Vector3.Distance(transform.position, centerPosition); //distance from flag to range
        if (distance > radius) //it is already within the circle.
        {
            Vector3 fromOriginToObject = transform.position - centerPosition; //flag - range
            fromOriginToObject *= radius / distance; //Multiply by radius //Divide by Distance
            transform.position = centerPosition + fromOriginToObject; //range + all that Math

        }

    }
}
