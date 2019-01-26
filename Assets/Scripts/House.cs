using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Shelter {

    private int capacity = 2;
    private int energyReq = 1;

    public override int Capacity {
        get;
        protected set;
    }

	// Use this for initialization
	void Start () {
        Capacity = 4;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
