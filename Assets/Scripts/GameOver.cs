using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour {

    public TextMeshProUGUI score;
    private string scoreTxt;

	void Start () {
        scoreTxt = "– " + GameManager.Instance.year.ToString() + " –";
        score.text = scoreTxt;

	}
}
