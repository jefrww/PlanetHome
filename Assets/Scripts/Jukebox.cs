using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour {

	public GameManager controller;
	private float co2;
	private AudioSource neutral, happy, sad;
	void Start () {
		
		happy = GameObject.Find("MusicHappy").GetComponent<AudioSource>();
		neutral = GameObject.Find("MusicNeutral").GetComponent<AudioSource>();
		sad = GameObject.Find("MusicSad").GetComponent<AudioSource>();
		
		happy.volume = 1.0f;
		neutral.volume = 1.0f;
		sad.volume = 0.0f;
	}
	
	void Update () {
		co2 = ((float) controller.pollution / 250f) * 100f;

		happy.volume = 1.0f - co2 / 30.0f;
		neutral.volume = 0.0f + (90 - co2) / 20f;
		sad.volume = 1.0f - (90 - co2) / 20f;

	}
}
