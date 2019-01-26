using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private CanvasManager HUD;
    private Player player;

    public int population = 2;
    public int populationCap = 1024;
    public int populationGrowthTime = 1;
    public float populationGrowthRate = 1.2f;
    public int populationSpawnTime = 0;
    public int pollution = 0;
    public int pollutionRate;
    public int credits = 0;
    public int creditRate;
    public int power = 0;
    private bool changedPopulationCap = false, changedPollutionRate = false;

    private float elapsedTime = 0;
    private List<Shelter> shelters;
    private List<Tree> trees;
    private List<Factory> factories;

    void Awake()
    {
        Instance = this;
        shelters = new List<Shelter>();
        trees = new List<Tree>();
        factories = new List<Factory>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bool changedHudValue = false;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= populationSpawnTime)
        {
            populationSpawnTime += populationGrowthTime;
            Debug.Log("Updated Population at: " + Time.time);
            UpdatePopulation();
            UpdatePollution();
            changedHudValue = true;
        }
        if (changedPopulationCap)
        {
            UpdatePopulationCap();
            changedPopulationCap = false;
            changedHudValue = true;
        }

        if (changedPollutionRate)
        {
            UpdatePollutionRate();
            changedPollutionRate = false;
            changedHudValue = true;
        }
        
        if(changedHudValue) HUD.UpdateHUD(population, populationCap, pollution, credits, power);

    }

    public void AddShelter(Shelter shelter)
    {
        shelters.Add(shelter);
        changedPopulationCap = true;
    }

    public void AddTree(Tree tree)
    {
        Debug.Log("Added Tree to Tree-List.");
        trees.Add(tree);
        changedPollutionRate = true;
    }

    public void AddFactory(Factory factory)
    {
        factories.Add(factory);
        changedPollutionRate = true;
    }

    public void AddCanvas(CanvasManager canvas)
    {
        this.HUD = canvas;
    }

    public void AddPlayer(Player player)
    {
        this.player = player;
    }

    public void UpdatePopulation()
    {
        Debug.Log(population);
        Debug.Log(populationGrowthRate.ToString("0.00"));
        population += (int)(population * populationGrowthRate);
        Debug.Log("Population increased to: " + population);
    }

    public void UpdatePopulationCap()
    {
        int cap = 0;
        foreach (Shelter s in shelters)
        {
            cap += s.capacity;
        }
        populationCap = cap;
    }

    public void UpdatePollution()
    {
        pollution += pollutionRate;
    }

    public void UpdatePollutionRate()
    {
        int negative = 0, positive = 0;
        foreach (Tree t in trees)
        {
            negative += t.pollutionRate;
        }

        foreach (Factory f in factories)
        {
            positive += f.pollutionRate;
        }
        pollutionRate = negative + positive;
    }
}