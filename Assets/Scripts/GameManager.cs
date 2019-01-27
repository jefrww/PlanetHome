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
    public int populationGrowthTime = 5;
    public float populationGrowthRate = 1.2f;
    public int populationSpawnTime = 0;
    public float income = 0.5f;
    public int pollution = 0;
    public int pollutionRate;
    public int credits = 0;
    public int creditRate = 1;
    public int tick = 1;
    public int tickTimer = 0;
    public int power = 0;
    public float powerPerCitizen = 1f;
    private bool changedPopulationCap = false, changedPollutionRate = false;
    public int year = 1960;

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
        if (elapsedTime >= tickTimer)
        {
            tickTimer += tick;
            UpdateCredits();
            UpdatePollutionRate();
            UpdatePollution();
            UpdatePower();
            changedHudValue = true;
        }
        if (elapsedTime >= populationSpawnTime)
        {
            populationSpawnTime += populationGrowthTime;
            UpdatePopulation();
            UpdateCreditRate();
            changedHudValue = true;
        }
        if (changedPopulationCap)
        {
            UpdatePopulationCap();
            changedPopulationCap = false;
            changedHudValue = true;
        }
        if (changedHudValue) HUD.UpdateHUD(population, populationCap, pollution, credits, power, year);
        selectBuilding();
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
        if (populationCap - population > 10)
        {
            population += 4;
        }
        else
        {
            population += 2; //= (int)System.Math.Ceiling(population*populationGrowthRate);
        }
        year++;
        //population+= 2; //= (int)System.Math.Ceiling(population*populationGrowthRate);
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
        //if (pollution < 0) pollution = 0;
    }

    public void UpdatePollutionRate()
    {
        int negative = 0, positive = 0;
        /*foreach (Tree t in trees)
        {
            negative += t.pollutionRate;
        }*/
        negative = (int)(System.Math.Log(trees.Count + 1) * -10);
        foreach (Factory f in factories)
        {
            positive += f.pollutionRate;
        }
        positive += (int)System.Math.Ceiling(population * population * 0.002f);
        pollutionRate = negative + positive;
    }

    public void UpdateCreditRate()
    {
        if (population > populationCap)
        {
            creditRate = (int)(populationCap * income);
        }
        else
        {
            creditRate = (int)(population * income);
        }
    }

    public void UpdatePower()
    {
        power = 0;
        foreach (Factory f in factories)
        {
            power += f.powerRate;
        }
        if (population > populationCap)
        {
            power -= (int)(populationCap * powerPerCitizen);
        }
        else
        {
            power -= (int)(population * powerPerCitizen);
        }
    }

    public void UpdateCredits()
    {
        credits += creditRate;
    }

    public void BuyBuilding(int price)
    {
        credits -= price;
    }
    public void selectBuilding()
    {
        if (Input.GetKeyDown("q"))
        {
            player.SetSelected(Player.ePlaceable.None);
        }
        if (Input.GetKeyDown("w"))
        {
            player.SetSelected(Player.ePlaceable.Tree);
        }
        if (Input.GetKeyDown("e"))
        {
            player.SetSelected(Player.ePlaceable.House);
        }
        if (Input.GetKeyDown("r"))
        {
            player.SetSelected(Player.ePlaceable.Factory);
        }
    }
}