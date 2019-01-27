using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour {

	public GameManager controller;
	int range = 285;
	RectTransform rt;

	float rangebit;
	int min = -130;
	int max = 155;

	float co2percentage;
	float ypos;

	int co2max = 250;

	int co2;

	void Start () {
		co2 = controller.pollution;
		rt = this.transform.GetComponent<RectTransform>();
		rangebit = (float) range / 100f;
	}

	void Update () {
		co2 = controller.pollution;
		if (co2 < 0) {
			co2 = 0;
		}
		co2percentage = ((float) co2 / (float) co2max) * 100f;
		ypos = (float) min + co2percentage * rangebit;
		Debug.Log(ypos);
		rt.anchoredPosition = new Vector3(rt.anchoredPosition.x, ypos);
	}
}
