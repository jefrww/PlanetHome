using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	
	public int population = 2;
	public int populationCap = 1024;
	public int populationGrowthTime = 1;
	public float populationGrowthRate = 1.2f;
	public int populationSpawnTime = 0;
	public int pollution;
	public int pollutionRate;
	public int credits;
	public int creditRate;
	public int power;
	private bool changedPopulationCap = false, changedPollutionRate = false;

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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time >= populationSpawnTime)
		{
			populationSpawnTime += populationGrowthTime;
			Debug.Log("Updated Population at: " + Time.time);
			UpdatePopulation();
		}
		if (changedPopulationCap)
		{
			UpdatePopulationCap();
			changedPopulationCap = false;
		}

		if (changedPollutionRate)
		{
			UpdatePollutionRate();
			changedPollutionRate = false;
		}
		
	}

	public void AddShelter(Shelter shelter)
	{
		shelters.Add(shelter);
		changedPopulationCap = true;
	}

	public void AddTree(Tree tree)
	{
		trees.Add(tree);
		changedPollutionRate = true;
	}

	public void AddFactory(Factory factory)
	{
		factories.Add(factory);
		changedPollutionRate = true;
	}

	public void UpdatePopulation()
	{
		Debug.Log(population);
		Debug.Log(populationGrowthRate.ToString("0.00"));
		population += (int)(population*populationGrowthRate);
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
