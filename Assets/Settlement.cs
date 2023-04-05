using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//结算界面

public class Settlement : MonoBehaviour{
	public GameObject destroyed;
	public GameObject missed;
	public GameObject survival_time;
	void Awake(){
		TextMeshProUGUI Text=destroyed.GetComponent<TextMeshProUGUI>();
		Text.text="Destroyed enemies: "+global.score;
		Text=missed.GetComponent<TextMeshProUGUI>();
		Text.text="Missed enemies: "+global.miss;
		Text=survival_time.GetComponent<TextMeshProUGUI>();
		Text.text="Survival time: "+Mathf.RoundToInt(global.survival_time)+"s";
	}
}
