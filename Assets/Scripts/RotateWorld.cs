using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{


    public float horizontalSpeed = 20.0F;
    public float verticalSpeed = 20.0F;

    void Update()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(0))
        {
            transform.Rotate(v, h, 0);
        }
    }
}
 