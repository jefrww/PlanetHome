using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private CanvasManager HUD;
    private Player player;
    private bool hardStop = false;

    public int population = 2;
    public int populationCap = 0;
    public int populationGrowthTime = 5;
    //public float populationGrowthRate = 1.2f;
    public int populationSpawnTime = 0;
    public float income = 0.2f;
    public int pollution = 0;
    public int pollutionRate = 0;
    public int credits = 200;
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

    public void ResetInitialState()
    {
        population = 2;
        populationCap = 0;
        populationGrowthTime = 5;
        //populationGrowthRate = 1.2f;
        populationSpawnTime = 0;
        income = 0.2f;
        pollution = 0;
        pollutionRate = 0;
        credits = 200;
        creditRate = 1;
        tick = 1;
        tickTimer = 0;
        power = 0;
        powerPerCitizen = 1f;
        changedPopulationCap = false;
        changedPollutionRate = false;
        year = 1960;
        elapsedTime = 0;
        shelters = new List<Shelter>();
        trees = new List<Tree>();
        factories = new List<Factory>();
    }

    void Awake()
    {
        Instance = this;
        shelters = new List<Shelter>();
        trees = new List<Tree>();
        factories = new List<Factory>();
        DontDestroyOnLoad(this);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!hardStop) {
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
            //if (changedHudValue) HUD.UpdateHUD(population, populationCap, pollution, credits, power, year);
            selectBuilding();
            if (GameIsOver()) {
                hardStop = true;
                SceneManager.LoadScene("GameOver");
            }
        }
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
        if (pollution < 0) pollution = 0;
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
            player.SetSelected(Player.ePlaceable.Skyscraper);
        }
        if (Input.GetKeyDown("t"))
        {
            player.SetSelected(Player.ePlaceable.Factory);
        }
    }

    public bool GameIsOver()
    {
        int overpopulation = population - populationCap;
        int tenPercent = (populationCap / 100) * 10;
        if (overpopulation > 10 && overpopulation > tenPercent) return true;
        else if (credits < -10) return true;
        else if (power < -10) return true;
        else if (pollution >= 250) return true;
        else
        {
            return false;
        }
    }
}