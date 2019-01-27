using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    private Text score;
    private string scoreTxt;

	void Start () {
        scoreTxt = "– " + GameManager.Instance.year.ToString() + " –";
        score = GetComponent<Text>();
        score.text = scoreTxt;

	}
}
