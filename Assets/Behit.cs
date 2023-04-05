using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自机被击中

public class Behit : MonoBehaviour{
	void OnTriggerEnter2D(Collider2D other){ //被击中
		if (global.debug) return;
		global.gameover=true;
		Destroy(gameObject); //销毁自机判定点
		Destroy(transform.parent.gameObject); //销毁自机
		StartCoroutine(global.loadScene("GameOver"));
	}
}
