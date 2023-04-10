using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌机基础脚本

public class Enemy : MonoBehaviour{
	public GameObject Bullet;
	public GameObject CircleBullet;
	public GameObject Laser;
	public int totalhp=10; //总血量
	public int hp=10; //当前血量
	public float speed=3; //飞行速度
	public Vector3 goalpos; //停止位置
	public bool reach=false,stop=false; //是否到达停止位置
	void MoveTowards(){ //朝着停止位置前进
		if (reach) return;
		Vector3 pos=transform.position+(goalpos-transform.position).normalized*speed;
		transform.position=Vector3.Lerp(transform.position,pos,Time.deltaTime);
		if (Vector3.Distance(transform.position,goalpos)<0.1f) reach=true;
	}
	public float stoptimecount=0;
	public float stoptime=5; //一定时间后敌机自行撤退
	void Update(){
		if (global.gameover) return; //游戏已经结束则不再继续判定
		MoveTowards();
		if (reach){
			if (stop){
				global.miss++; //未击毁敌机
				Destroy(gameObject); //敌机撤退完成，销毁敌机
			}
			stoptimecount+=Time.deltaTime;
			if (!stop && stoptimecount>stoptime){ //敌机开始撤退
				goalpos=new Vector3(transform.position.x,6,0);
				reach=false;
				stop=true;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (global.gameover) return; //游戏已经结束则不再继续判定
		Bullet now=other.gameObject.GetComponent<Bullet>();
		if (now!=null && now.type==0){ //子弹为自机子弹才判定击中
			hp--;
			if (hp<=0){
				global.score++; //击毁敌机
				Destroy(gameObject);
			}
		}
	}
}
