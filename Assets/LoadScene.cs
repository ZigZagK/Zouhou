using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//跳转场景脚本

public class LoadScene : MonoBehaviour{
	public string scenename;
	public string bossname;
	public void Switch(){
		global.bossname=bossname;
		StartCoroutine(global.loadScene(scenename));
	}
}
