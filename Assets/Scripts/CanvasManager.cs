using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private Text population, pollution, power, credits;
    private Button buildTree;
    // Use this for initialization
    void Start()
    {
        GameManager.Instance.AddCanvas(this);
        // Load the Arial font from the Unity Resources folder.
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        // Create Canvas GameObject.
        GameObject UI = new GameObject();
        UI.name = "Canvas";
        UI.AddComponent<Canvas>();
        UI.AddComponent<CanvasScaler>();
        UI.AddComponent<GraphicRaycaster>();
        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = UI.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // Create Text fields
        GameObject populationField = new GameObject();
        GameObject pollutionField = new GameObject();
        GameObject creditsField = new GameObject();
        GameObject powerField = new GameObject();

        populationField.transform.parent = UI.transform;
        populationField.name = "populationField";
        pollutionField.transform.parent = UI.transform;
        pollutionField.name = "pollutionField";
        creditsField.transform.parent = UI.transform;
        creditsField.name = "creditsField";
        powerField.transform.parent = UI.transform;
        powerField.name = "powerField";

        populationField.AddComponent<Text>();
        pollutionField.AddComponent<Text>();
        creditsField.AddComponent<Text>();
        powerField.AddComponent<Text>();
        // Set Text component properties.
        population = populationField.GetComponent<Text>();
        pollution = pollutionField.GetComponent<Text>();
        credits = creditsField.GetComponent<Text>();
        power = powerField.GetComponent<Text>();

        population.font = arial;
        pollution.font = arial;
        credits.font = arial;
        power.font = arial;

        population.text = "population";
        pollution.text = "pollution";
        credits.text = "credits";
        power.text = "power";

        population.fontSize = 20;
        pollution.fontSize = 20;
        credits.fontSize = 20;
        power.fontSize = 20;

        population.alignment = TextAnchor.UpperLeft;
        pollution.alignment = TextAnchor.UpperLeft;
        credits.alignment = TextAnchor.UpperLeft;
        power.alignment = TextAnchor.UpperLeft;
        // Provide Text position and size using RectTransform.
        RectTransform populationTransform;
        RectTransform pollutionTransform;
        RectTransform creditsTransform;
        RectTransform powerTransform;

        populationTransform = population.GetComponent<RectTransform>();
        pollutionTransform = pollution.GetComponent<RectTransform>();
        creditsTransform = credits.GetComponent<RectTransform>();
        powerTransform = power.GetComponent<RectTransform>();

        populationTransform.localPosition = new Vector3(-100, 0, 0);
        pollutionTransform.localPosition = new Vector3(-100, -20, 0);
        creditsTransform.localPosition = new Vector3(-100, -40, 0);
        powerTransform.localPosition = new Vector3(-100, -60, 0);

        populationTransform.sizeDelta = new Vector2(600, 200);
        pollutionTransform.sizeDelta = new Vector2(600, 200);
        creditsTransform.sizeDelta = new Vector2(600, 200);
        powerTransform.sizeDelta = new Vector2(600, 200);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHUD(int populationVal, int populationCapVal, int pollutionVal, int creditVal, int powerVal, int year)
    {
        population.text = "Population: " + populationVal.ToString() + "/" + populationCapVal.ToString();
        pollution.text = "Pollution: " + pollutionVal.ToString();
        credits.text = "Credits: " + creditVal.ToString();
        power.text = "Power: " + powerVal.ToString();
    }
}