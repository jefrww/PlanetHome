using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour {

	AudioSource house, tree, factory;
	public void Start() {
		house = GameObject.Find("SFX_House").GetComponent<AudioSource>();
		tree = GameObject.Find("SFX_Tree").GetComponent<AudioSource>();
		factory = GameObject.Find("SFX_Factory").GetComponent<AudioSource>();
	}

	public void PlayHouse() {
		house.Play();
	}

	public void PlayTree() {
		tree.Play();
	}

	public void PlayFactory() {
		factory.Play();
	}
}
