using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject tree, house, factory;
    public Material previewMaterial;
    public enum ePlaceable { None, Tree, House, Factory };

    private Camera mainCam;
    private GameObject planet;
    private GameObject previewObj;
    private bool canPlace;
    private Color green = new Color(0, 1, 0, .5f);
    private Color red = new Color(1, 0, 0, .5f);
    private ePlaceable selected = ePlaceable.None;

    void Start()
    {
        GameManager.Instance.AddPlayer(this);
        mainCam = Camera.main;
        planet = GameObject.FindWithTag("Planet");
        previewMaterial.color = red;
    }

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

    public void SetSelected(ePlaceable select)
    {
        selected = select;
        UpdatePreviewObject();
    }

    private void PreviewPlacement()
    {
        if (selected == ePlaceable.None && previewObj != null)
        {
            Destroy(previewObj);
        }
        else if (selected != ePlaceable.None)
        {
            if (previewObj == null)
            {
                UpdatePreviewObject();
            }

            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (previewObj.GetComponent<Placeable>().collisions == 0)
                {
                    previewMaterial.color = green;
                    canPlace = true;
                }
                else
                {
                    previewMaterial.color = red;
                    canPlace = false;
                }
                var objRot = Quaternion.LookRotation(hit.normal);
                previewObj.transform.position = hit.point;
                previewObj.transform.rotation = objRot;

            }
            else
            {
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
            if (canPlace && selected != ePlaceable.None)
            {
                Debug.DrawLine(mainCam.transform.position, hit.point, Color.red);
                Debug.DrawRay(mainCam.transform.position, mainCam.transform.position - hit.point, Color.green);
                var objRot = Quaternion.LookRotation(hit.normal);

                var placedObj = InstantiateSelected();
                placedObj.transform.position = hit.point;
                placedObj.transform.rotation = objRot;
                placedObj.transform.parent = planet.transform;
                switch (selected)
                {
                    case ePlaceable.Tree:
                        {
                            placedObj.GetComponent<Tree>().Place();
                            break;
                        }
                    case ePlaceable.House:
                        {
                            placedObj.GetComponent<Shelter>().Place();
                            break;
                        }
                    case ePlaceable.Factory:
                        {
                            placedObj.GetComponent<Factory>().Place();
                            break;
                        }
                    default:
                        {
                            Debug.Log("Error: No valid Placeable selected!");
                            break;
                        }
                }
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
            case ePlaceable.Factory:
                {
                    return (GameObject)Instantiate(factory);
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
        if (selected != ePlaceable.None)
        {
            previewObj = InstantiateSelected();
            Physics.IgnoreCollision(previewObj.GetComponent<Collider>(), planet.GetComponent<Collider>());
            previewObj.GetComponent<MeshRenderer>().material = previewMaterial;
            previewObj.layer = 2;
        }
    }

}
