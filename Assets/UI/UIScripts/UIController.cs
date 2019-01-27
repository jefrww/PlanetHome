using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour {

	private Color normal, warning;

	public GameManager controller;
	public TextMeshProUGUI yearTXT;
	public TextMeshProUGUI popsTXT;
	public TextMeshProUGUI powerTXT;
	public TextMeshProUGUI moneyTXT;

	private string popSeparator = " / ";
	void Start () {
		normal = new Color32(250, 230, 187, 255);
		warning = new Color32(255, 191, 0, 255);
	}

	void Update () {
		//cheesy, but not a single callback
		yearTXT.text = controller.year.ToString();
		popsTXT.text = controller.population.ToString() + popSeparator + controller.populationCap.ToString();

		if (controller.population > controller.populationCap) {
			popsTXT.color = warning;
		} else {
			popsTXT.color = normal;
		}

		if (controller.power < 0) {
			powerTXT.color = warning;
			powerTXT.text = controller.power.ToString();
		} else if (controller.power > 0) {
			powerTXT.color = normal;
			powerTXT.text = "+" + controller.power.ToString();
		} else {
			powerTXT.color = normal;
			powerTXT.text = controller.power.ToString();
		}

		moneyTXT.text = controller.credits.ToString();
		if (controller.credits < 0) {
			moneyTXT.color = warning;
		} else {
			moneyTXT.color = normal;
		}
		
	}
}
