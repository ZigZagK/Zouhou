using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌机基础脚本

public class Enemy : MonoBehaviour{
	public float totalhp=10; //总血量
	public float hp=10; //当前血量
	public float speed=3; //飞行速度
	public Vector3 goalpos; //停止位置
	public bool reach=false,stop=false; //是否到达停止位置
	void MoveTowards(){ //朝着停止位置前进
		if (reach) return;
		if (Vector2.Distance(transform.position,goalpos)<=Time.deltaTime*speed){
			transform.position=goalpos;
			reach=true;
			return;
		}
		transform.position+=(goalpos-transform.position).normalized*speed*Time.deltaTime;
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
				goalpos=new Vector2(transform.position.x,6);
				reach=false;
				stop=true;
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other){ //一次性碰撞
		if (global.gameover) return; //游戏已经结束则不再继续判定
		if (hp<=0) return;
		Bullet now=other.gameObject.GetComponent<Bullet>();
		if (now!=null && now.type==0 && now.damagetype==0){ //子弹为自机子弹，并且是直伤子弹，才判定击中
			hp-=now.damage;
			if (hp<=0){
				global.score++; //击毁敌机
				Destroy(gameObject);
			}
		}
	}
	void OnTriggerStay2D(Collider2D other){ //碰撞保持
		if (global.gameover) return; //游戏已经结束则不再继续判定
		if (hp<=0) return;
		Bullet now=other.gameObject.GetComponent<Bullet>();
		if (now!=null && now.type==0 && now.damagetype==1){ //子弹为自机子弹，并且是帧伤子弹，才判定击中
			hp-=now.damage*Time.deltaTime;
			if (hp<=0){
				global.score++; //击毁敌机
				Destroy(gameObject);
			}
		}
	}
}
