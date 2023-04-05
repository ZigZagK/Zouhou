using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//生存模式脚本

public class SurvivalMode : MonoBehaviour{
	public GameObject Enemy;
	void Awake(){
		global.GlobalInfoInit();
	}
	void MakeEnemy(){ //产生随机发射子弹的敌机
		float posx=Random.Range(-3.5f,3.5f);
		GameObject enemy=Instantiate(Enemy,new Vector3(posx,5,0),Quaternion.identity);
		enemy.AddComponent<RandomEnemy>();
		Enemy enemycs=enemy.GetComponent<Enemy>();
		enemycs.goalpos=new Vector3(posx,5-Random.Range(1f,2f),0);
	}
	private float timecount;
	private float newenemydeltatime=0; //产生敌机时间间隔
	void Update(){
		if (global.gameover) return; //游戏已经结束则不再继续判定
		global.survival_time+=Time.deltaTime;
		timecount+=Time.deltaTime;
		if (timecount>newenemydeltatime){
			MakeEnemy();
			timecount=0;
			newenemydeltatime=Random.Range(0.5f,1f);
		}
	}
}
