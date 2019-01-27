using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWorld : MonoBehaviour
{
    public float horizontalSpeed = 20.0F;
    public float verticalSpeed = 20.0F;
    bool isRotating;
    RaycastHit hit;
    Ray ray;

    private Camera cam;
    GameObject planet;

    void Start()
    {
        planet = GameObject.FindWithTag("Planet");
        cam = Camera.main;
        isRotating = false;
    }

    void Update()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(1))
        {
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!isRotating)
            {

                if (Physics.Raycast(ray, out hit))
                {
                    isRotating = true;
                    transform.Rotate(new Vector3(v, -h, 0), Space.World);
                }
            }
            else
            {
                transform.Rotate(new Vector3(v, -h, 0), Space.World);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

    }
}