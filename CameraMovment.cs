using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour {

    public float panSpeed = 20f;

    public Vector2 penLimit;

    public float scrollpeed = 20f;

    public float mixZ;
    public float minZ;

    void Update ()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w"))
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        //float scroll = Input.GetAxis("Mouse Scrollwheel");
        //pos.z += scroll * panSpeed * 100f * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, -penLimit.y, penLimit.y);
        pos.z = Mathf.Clamp(pos.z, mixZ, minZ);
        pos.x = Mathf.Clamp(pos.x, -penLimit.x, penLimit.x);

        transform.position = pos;


        

    }


    
}
