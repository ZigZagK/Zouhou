using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏脚本

public class Game : MonoBehaviour{
	public static float xmin,xmax,ymin,ymax; //屏幕边界
	public GameObject Enemy; //敌机预设体
	void Awake(){
		//获取画面边界
		Vector2 leftbottom=Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 righttop=Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		xmax=righttop.x;
		xmin=leftbottom.x;
		ymax=righttop.y;
		ymin=leftbottom.y;
		//全局信息初始化
		GlobalInfo.miss=GlobalInfo.score=0;
		GlobalInfo.survival_time=0;
	}
	void MakeEnemy(){ //产生敌机
		float posx=Random.Range(-3.5f,3.5f);
		GameObject enemy=Instantiate(Enemy,new Vector3(posx,5,0),Quaternion.identity);
		Enemy enemycs=enemy.GetComponent<Enemy>();
		enemycs.goalpos=new Vector3(posx,5-Random.Range(1f,2f),0);
	}
	private float timecount;
	private float newenemydeltatime=0; //产生敌机时间间隔
	void Update(){
		GlobalInfo.survival_time+=Time.deltaTime;
		timecount+=Time.deltaTime;
		if (timecount>newenemydeltatime){
			MakeEnemy();
			timecount=0;
			newenemydeltatime=Random.Range(0.5f,1f);
		}
	}
}
