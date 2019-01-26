using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Placeable
{
	public int pollutionRate;
	void Awake()
	{
		GameManager.Instance.AddTree(this);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
