using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//符卡1 螺旋交叉弹 敌机脚本

public class Boss1 : MonoBehaviour{
	private Enemy enemycs;
	private float cardtime=100; //符卡时间
	void Awake(){
		enemycs=gameObject.GetComponent<Enemy>();
		transform.position=new Vector3(0,5,0);
		enemycs.goalpos=new Vector3(0,2,0);
		enemycs.totalhp=enemycs.hp=250;
		enemycs.stoptime=cardtime;
	}
	void ShootBullet(Vector3 pos,Vector3 forwards,float speed,Color color){
		GameObject bullet=Instantiate(enemycs.Bullet,pos,Quaternion.identity);
		SpriteRenderer sr=bullet.GetComponent<SpriteRenderer>();
		sr.color=color;
		Bullet bulletcs=bullet.GetComponent<Bullet>();
		bulletcs.speed=speed;
		bulletcs.movetowards=forwards;
		bulletcs.xmin=global.xmin;bulletcs.xmax=global.xmax;
		bulletcs.ymin=global.ymin;bulletcs.ymax=global.ymax;
		bulletcs.type=1;
	}
	Vector3 Rotate(Vector3 v,float angle){ //向量v逆时针旋转angle
		float sina=Mathf.Sin(angle),cosa=Mathf.Cos(angle);
		return new Vector3(v.x*cosa-v.y*sina,v.x*sina+v.y*cosa);
	}
	//子弹分为快速弹和慢速弹
	private Vector3 fastforwards=Vector3.up;
	private Color fastcolor=new Color32(104,212,255,255);
	private Vector3 slowforwards=Vector3.up;
	private Color slowcolor=new Color32(255,175,7,255);
	private float fasttimecount=0;
	private float fastdeltatime=0.05f; //蓝子弹发射时间间隔
	private float slowtimecount=0;
	private float slowdeltatime=0.075f; //绿子弹发射时间间隔
	void Fire(){ //发射子弹
		if (enemycs.reach){ //到达位置后开始发射子弹
			fasttimecount+=Time.deltaTime;
			if (fasttimecount>fastdeltatime){
				for (int i=0;i<8;i++)
					ShootBullet(transform.position,Rotate(fastforwards,Mathf.PI*i/4),4f,fastcolor);
				fastforwards=Rotate(fastforwards,0.15f);
				fasttimecount=0;
			}
			slowtimecount+=Time.deltaTime;
			if (slowtimecount>slowdeltatime){
				for (int i=0;i<4;i++)
					ShootBullet(transform.position,Rotate(slowforwards,Mathf.PI*i/2),2f,slowcolor);
				slowforwards=Rotate(slowforwards,-0.25f);
				slowtimecount=0;
			}
		}
	}
	void Update(){
		Fire();
	}
}
