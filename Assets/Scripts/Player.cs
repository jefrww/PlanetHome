using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject tree, house;
    public Material previewMaterial;
    public enum ePlaceable { None, Tree, House };
    public ePlaceable selected = ePlaceable.Tree;

    private Camera mainCam;
    private GameObject planet;
    private GameObject previewObj;
    private bool canPlace = true;
    private Color green = new Color(0, 1, 0, .5f);
    private Color red = new Color(1, 0, 0, .5f);

    // Use this for initialization
    void Start()
    {
        mainCam = Camera.main;
        planet = GameObject.FindWithTag("Planet");
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
        selected = (ePlaceable)System.Enum.Parse(typeof(ePlaceable), select);
        UpdatePreviewObject();
    }

    private void PreviewPlacement()
    {
        if (selected == ePlaceable.None && previewObj != null)
        {
            Destroy(previewObj);
        }
        else
        {
            if (previewObj == null)
            {
                UpdatePreviewObject();
            }

            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (IsValidLocation(hit))
                {
                    if (previewObj.GetComponent<Placeable>().collisions == 0) {
                        previewMaterial.color = green;
                        canPlace = true;
                    } else {
                        previewMaterial.color = red;
                        canPlace = false;
                    }
                    var objRot = Quaternion.LookRotation(hit.normal);
                    previewObj.transform.position = hit.point;
                    previewObj.transform.rotation = objRot;
                }
            } else {
                Destroy(previewObj);
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
            if(IsValidLocation(hit) && canPlace)
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
            case ePlaceable.Tree:
                {
                    return (GameObject)Instantiate(tree);
                }
            case ePlaceable.House:
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
        return true;
    }

    private void UpdatePreviewObject()
    {
        Destroy(previewObj);
        previewObj = InstantiateSelected();
        Physics.IgnoreCollision(previewObj.GetComponent<Collider>(), planet.GetComponent<Collider>());
        previewObj.GetComponent<MeshRenderer>().material = previewMaterial;
        previewObj.layer = 2;
    }

}
