using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//符卡1 螺旋交叉弹 敌机脚本

public class Boss1 : MonoBehaviour{
	private Enemy enemycs;
	private BulletSet bs;
	private float cardtime=60; //符卡时间
	void Awake(){
		enemycs=gameObject.GetComponent<Enemy>();
		transform.position=new Vector2(0,5);
		enemycs.goalpos=new Vector2(0,2);
		enemycs.totalhp=enemycs.hp=300;
		enemycs.stoptime=cardtime;
		bs=gameObject.GetComponent<BulletSet>();
	}
	void ShootBullet(Vector2 pos,Vector2 forward,float speed,Color color){
		GameObject bullet=Instantiate(bs.Bullet,pos,Quaternion.identity);
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
	//子弹分为快速弹和慢速弹
	private Vector2 fastforward=Vector2.up;
	private Color fastcolor=new Color32(104,212,255,255);
	private Vector2 slowforward=Vector2.up;
	private Color slowcolor=new Color32(255,175,7,255);
	private float fasttimecount=0;
	private float fastdeltatime=0.05f; //快速弹发射时间间隔
	private float slowtimecount=0;
	private float slowdeltatime=0.075f; //慢速弹发射时间间隔
	void Fire(){ //发射子弹
		if (enemycs.reach){ //到达位置后开始发射子弹
			fasttimecount+=Time.deltaTime;
			if (fasttimecount>fastdeltatime){
				for (int i=0;i<8;i++)
					ShootBullet(transform.position,Rotate(fastforward,Mathf.PI*i/4),4f,fastcolor);
				fastforward=Rotate(fastforward,0.15f);
				fasttimecount=0;
			}
			slowtimecount+=Time.deltaTime;
			if (slowtimecount>slowdeltatime){
				for (int i=0;i<4;i++)
					ShootBullet(transform.position,Rotate(slowforward,Mathf.PI*i/2),2f,slowcolor);
				slowforward=Rotate(slowforward,-0.25f);
				slowtimecount=0;
			}
		}
	}
	void Update(){
		Fire();
	}
}
