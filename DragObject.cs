using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

    private Vector3 Offset;
    float distance = 10.0f;
    Barracks barracks;
    Vector2 relative;


    private void Start()
    {
        barracks = GameObject.Find("BarracksSprite").GetComponent<Barracks>();

    }
    private void Update()
    {
        Vector3 pos = transform.position;
        double Px = pos.x, Py = pos.y;
        relative = new Vector2(transform.position.x - barracks.transform.position.x, transform.position.y - barracks.transform.position.y);
        float DistanceToBarraacks = Vector2.Distance(transform.position, barracks.transform.position);
        pos.z = -1;
        if (DistanceToBarraacks >= barracks.range)
        {
            if (Mathf.Abs(pos.x) >= Mathf.Abs(pos.y))
            {
                if (relative.x < 0)
                    pos.x += 0.1f;
                //Px += 1;
                else
                    pos.x -= 0.1f;
            }
            else
            {
                if (relative.y < 0)
                    pos.y += 0.1f;
                else
                    pos.y -= 0.1f;
            }
            transform.position = pos;
        }


        Collider[] Colliders = Physics.OverlapSphere(transform.position, barracks.range);


    }

    void OnMouseDown()
    {
        
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y,7);
        Offset = transform.position - Camera.main.ScreenToWorldPoint(mousePos);
    }
    void OnMouseDrag()
    {
        Vector3 pos = transform.position;
        float DistanceToBarraacks = Vector2.Distance(pos, barracks.transform.position);
        if (DistanceToBarraacks <= barracks.range)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7);
            //transform.position = Camera.main.ScreenToWorldPoint(mousePos) + Offset;
            if(Vector2.Distance(Camera.main.ScreenToWorldPoint(mousePos) + Offset, barracks.transform.position) <= barracks.range)
            {
                Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePos);
                transform.position = objPosition;
            }
        }
        else
        {
            Vector3 fromOriginToObject = transform.position - barracks.rangeV; 
            fromOriginToObject *= barracks.range / distance;
            barracks.rangeV  =  transform.position - fromOriginToObject; 
        }

    }
           
}

