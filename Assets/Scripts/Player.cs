using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject tree1, tree2, house, skyscraper, factory;
    public Material previewMaterial;
    public enum ePlaceable { None, Tree, House, Factory, Skyscraper };

    private Camera mainCam;
    private GameObject planet;
    private GameObject previewObj;
    private bool canPlace;
    private Color green = new Color(0, 1, 0, .5f);
    private Color red = new Color(1, 0, 0, .5f);
    private ePlaceable selected = ePlaceable.None;
    private System.Random rng;

    private SFX sound;

    void Start()
    {
<<<<<<< HEAD
        rng = new System.Random();
=======
        sound = this.transform.GetComponent<SFX>();
>>>>>>> master
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
            if(CanBuy()) PlaceObject();
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

    public int GetCost(ePlaceable placeable)
    {
        switch (placeable)
        {
            case ePlaceable.Tree:
                {
                    return tree1.GetComponent<Tree>().price;
                }
            case ePlaceable.House:
                {
                    return house.GetComponent<Shelter>().price;
                }
            case ePlaceable.Skyscraper:
                {
                    return skyscraper.GetComponent<Shelter>().price;
                }
            case ePlaceable.Factory:
                {
                    return factory.GetComponent<Factory>().price;
                }
            default:
                {
                    return 0;
                }
        }
    }

    /*** Private Section ***/

    private void PreviewPlacement()
    {
        // Wenn kein Bauobjekt ausgewählt ist aber noch ein previewObject besteht
        if (selected == ePlaceable.None && previewObj != null)
        {
            Destroy(previewObj);
        }
        // Wenn ein Bauobjekt ausgewählt ist
        else if (selected != ePlaceable.None)
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // Falls die Preview vom Bauobjekt noch nicht instanziiert wurde
                if (previewObj == null && IsValidPosition(hit))
                {
                    UpdatePreviewObject();
                }
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
                            sound.PlayTree();
                            placedObj.GetComponent<Tree>().Place();
                            break;
                        }
                    case ePlaceable.House:
                        {
                            sound.PlayHouse();
                            placedObj.GetComponent<Shelter>().Place();
                            break;
                        }
                    case ePlaceable.Skyscraper:
                        {
                            sound.PlayHouse();
                            placedObj.GetComponent<Shelter>().Place();
                            break;
                        }
                    case ePlaceable.Factory:
                        {
                            sound.PlayFactory();
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
                    if (rng.Next(11) < 5)
                    {
                        return (GameObject) Instantiate(tree1);
                    }
                    else return (GameObject)Instantiate(tree2);
                }
            case ePlaceable.House:
                {
                    return (GameObject)Instantiate(house);
                }
            case ePlaceable.Factory:
                {
                    return (GameObject)Instantiate(factory);
                }
            case ePlaceable.Skyscraper:
                {
                    return (GameObject)Instantiate(skyscraper);
                }
            default:
                {
                    Debug.Log("Error: No valid Placeable selected!");
                    return null;
                }
        }
    }

    private bool IsValidPosition(RaycastHit hit) {
        if (hit.transform.CompareTag("Planet"))
        {
            return true;
        }
        return false;
    }

    private bool CanBuy()
    {
        return GetCost(selected) <= GameManager.Instance.credits;;
    }

    private void UpdatePreviewObject()
    {
        Destroy(previewObj);
        if (selected != ePlaceable.None)
        {
            previewObj = InstantiateSelected();
            previewObj.transform.position = new Vector3(0, 0, 0);
            Physics.IgnoreCollision(previewObj.GetComponent<Collider>(), planet.GetComponent<Collider>());
            var mats = previewObj.GetComponent<MeshRenderer>().materials;
            for (int i = 0; i < mats.Length; i++)
            {
                Debug.Log("Change Mat");
                mats[i] = previewMaterial;
            }
            previewObj.GetComponent<MeshRenderer>().materials = mats;
            previewObj.layer = 2;
        }
    }

}
