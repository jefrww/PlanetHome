using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {

    public int price;
    public int radius;
    public int collisions = 0;

    public void Place()
    {
	    Debug.Log("Used Default Place from Placeable.");
	    return;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        collisions ++;
    }
    private void OnCollisionExit(Collision col)
    {
        collisions --;
    }
}
