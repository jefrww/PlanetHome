using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : Placeable
{

    public int capacity;
    // Use this for initialization
    public void Place()
    {
        GameManager.Instance.AddShelter(this);
        GameManager.Instance.BuyBuilding(this.price);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}