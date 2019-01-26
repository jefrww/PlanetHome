using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject tree, house;

    private Camera mainCam;

    public enum Placeable { Tree, House };
    public Placeable selected = Placeable.Tree;

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }

    public void SetSelected(string select)
    {
        selected = (Placeable)System.Enum.Parse(typeof(Placeable), select);
    }

    private void PlaceObject()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(mainCam.transform.position, hit.point, Color.red);
            Debug.DrawRay(mainCam.transform.position, mainCam.transform.position - hit.point, Color.green);
            var startRot = Quaternion.LookRotation(hit.normal);

            switch (selected)
            {
                case Placeable.Tree:
                    {
                        Instantiate(tree, hit.point, startRot);
                        break;
                    }

                case Placeable.House:
                    {
                        Instantiate(house, hit.point, startRot);
                        break;
                    }
                default:
                    {
                        Debug.Log("Error: No Placeable selected!");
                        break;
                    }
            }
        }


    }
}
