using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//符卡3 用于揍人的纯粹弹幕 敌机脚本

public class Boss3 : MonoBehaviour{
	private Enemy enemycs;
	private BulletSet bs;
	private float cardtime=60; //符卡时间
	void Awake(){
		enemycs=gameObject.GetComponent<Enemy>();
		transform.position=new Vector2(0,5);
		enemycs.goalpos=new Vector2(0,3);
		enemycs.totalhp=enemycs.hp=300;
		enemycs.stoptime=cardtime;
		bs=gameObject.GetComponent<BulletSet>();
	}
	void ShootBullet(Vector2 pos,Vector2 forward,float speed,Color color){
		GameObject bullet=Instantiate(bs.CircleBullet,pos,Quaternion.identity);
		SpriteRenderer sr=bullet.GetComponent<SpriteRenderer>();
		sr.color=color;
		StraightBullet bulletcs=bullet.AddComponent<StraightBullet>();
		bulletcs.speed=speed;
		bulletcs.movetowards=forward;
		bulletcs.xmin=global.xmin;bulletcs.xmax=global.xmax;
		bulletcs.ymin=global.ymin;bulletcs.ymax=global.ymax;
	}
	Vector2 Rotate(Vector2 v,float angle){ //向量v逆时针旋转angle
		float sina=Mathf.Sin(angle),cosa=Mathf.Cos(angle);
		return new Vector2(v.x*cosa-v.y*sina,v.x*sina+v.y*cosa);
	}
	private Color color1=new Color32(104,212,255,255);
	private Color color2=new Color32(255,175,7,255);
	private Color color3=Color.green;
	private float timecount=0;
	private float deltatime=0f;
	private int total=70; //弹幕将圆等分为多少部分
	void Fire(){ //发射子弹
		if (enemycs.reach){ //到达位置后开始发射子弹
			timecount+=Time.deltaTime;
			if (timecount>deltatime){
				float angle=Random.Range(0f,3f);
				Color nowcolor=(angle<=1?color1:(angle<=2?color2:color3));
				Vector2 forward=new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
				for (int i=0;i<total;i++){
					Vector2 nowforward=Rotate(forward,Mathf.PI*i/total*2);
					ShootBullet(transform.position,nowforward,3f,nowcolor);
				}
				timecount=0;
				//发狂机制，时间或血量减少时弹幕发射间隔减小
				float f=(cardtime-enemycs.stoptimecount)/cardtime;
				if ((float)enemycs.hp/enemycs.totalhp<f) f=(float)enemycs.hp/enemycs.totalhp;
				deltatime=Mathf.Log(f+1,2)+1f;
			}
		}
	}
	void Update(){
		Fire();
	}
}
