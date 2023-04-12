using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//选择子机

public class SelectSubplane : MonoBehaviour{
	private TMP_Dropdown dp;
	private string[] subname={
		"TrackingSub",
		"LaserSub"
	};
	void Awake(){
		dp=gameObject.GetComponent<TMP_Dropdown>();
		dp.SetValueWithoutNotify(global.subtype);
		global.subname=subname[global.subtype];
	}
	public void ChooseSubplane(){
		global.subtype=dp.value;
		global.subname=subname[dp.value];
	}
}
