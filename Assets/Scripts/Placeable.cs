using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour {

    public int collisions = 0;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("COLLISION");
        collisions ++;
    }
    private void OnCollisionExit(Collision col)
    {
        collisions --;
    }
}
