using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹基础脚本

public class Bullet : MonoBehaviour{
	public int type=1; //子弹类型，0为自机，1为敌机
	public float damage=1; //子弹伤害
	public int damagetype=0; //伤害类型，0为直伤（一次性），1为帧伤（非一次性）
	void OnTriggerEnter2D(Collider2D other){
		if (damagetype==1) return; //非一次性子弹，不销毁
		if (type==0){ //与敌机相撞
			Enemy now=other.gameObject.GetComponent<Enemy>();
			if (now!=null) Destroy(gameObject);
		} else { //与自机相撞
			Player now=other.gameObject.GetComponent<Player>();
			if (now!=null) Destroy(gameObject);
		}
	}
}
