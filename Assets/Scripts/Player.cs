using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject tree, house;
    public Material previewMaterial;
    public enum Placeable { None, Tree, House };
    public Placeable selected = Placeable.Tree;

    private Camera mainCam;
    private GameObject previewObj;

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PreviewPlacement();
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }

    public void SetSelected(string select)
    {
        selected = (Placeable)System.Enum.Parse(typeof(Placeable), select);
        UpdatePreviewObject();
    }

    private void PreviewPlacement()
    {
        if (selected == Placeable.None && previewObj != null)
        {
            Destroy(previewObj);
        }
        else
        {
            if(previewObj == null)
            {
                UpdatePreviewObject();
            }

            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (IsValidLocation(hit))
                {
                    var objRot = Quaternion.LookRotation(hit.normal);
                    previewObj.transform.position = hit.point;
                    previewObj.transform.rotation = objRot;
                }
            }
        }
    }

    private void PlaceObject()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(IsValidLocation(hit));
            if(IsValidLocation(hit))
            {
                Debug.DrawLine(mainCam.transform.position, hit.point, Color.red);
                Debug.DrawRay(mainCam.transform.position, mainCam.transform.position - hit.point, Color.green);
                var objRot = Quaternion.LookRotation(hit.normal);

                var placedObj = InstantiateSelected();
                placedObj.transform.position = hit.point;
                placedObj.transform.rotation = objRot;
            }
        }
    }

    private GameObject InstantiateSelected()
    {
        switch (selected)
        {
            case Placeable.Tree:
                {
                    return (GameObject)Instantiate(tree);
                }
            case Placeable.House:
                {
                    return (GameObject)Instantiate(house);
                }
            default:
                {
                    Debug.Log("Error: No valid Placeable selected!");
                    return null;
                }
        }
    }

    private bool IsValidLocation(RaycastHit hit)
    {
        if (hit.transform.gameObject.CompareTag("Planet"))
        {
            return true;
        }
        else
            return false;
    }

    private void UpdatePreviewObject()
    {
        Destroy(previewObj);
        previewObj = InstantiateSelected();
        previewObj.GetComponent<MeshRenderer>().material = previewMaterial;
        previewObj.layer = 2;
    }

}
