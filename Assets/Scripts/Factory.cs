using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Placeable
{
	public int powerRate;
	public int pollutionRate;
	
	void Awake()
	{
		GameManager.Instance.AddFactory(this);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
