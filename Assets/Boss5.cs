using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//符卡5 BOSS也会走位 敌机脚本

public class Boss5 : MonoBehaviour{
	private Enemy enemycs;
	private BulletSet bs;
	private float xmin,xmax,ymin,ymax; //移动范围
	private float cardtime=60; //符卡时间
	void Awake(){
		enemycs=gameObject.GetComponent<Enemy>();
		transform.position=new Vector2(0,5);
		enemycs.speed=3;
		enemycs.goalpos=new Vector2(0,3);
		enemycs.totalhp=enemycs.hp=250;
		enemycs.stoptime=cardtime;
		bs=gameObject.GetComponent<BulletSet>();
		xmin=global.xmin+0.3f;xmax=global.xmax-0.3f;
		ymin=1;ymax=global.ymax-0.3f;
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
	private float xrange=2,yrange=1;
	Vector2 RandomPosition(Vector2 pos){ //随机附近位置
		float xl=pos.x-xrange,xr=pos.x+xrange,yl=pos.y-yrange,yr=pos.y+yrange;
		if (xl<xmin) xl=xmin;if (xr>xmax) xr=xmax;
		if (yl<ymin) yl=ymin;if (yr>ymax) yr=ymax;
		return new Vector2(Random.Range(xl,xr),Random.Range(yl,yr));
	}
	Vector2 RandomDirection(){ //随机角度发射
		float angle=Random.Range(0,Mathf.PI*2);
		return new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
	}
	private Color color=new Color32(104,212,255,255);
	private int total=30; //弹幕将圆等分为多少部分
	void Fire(){ //发射子弹
		if (enemycs.reach){ //到达位置后开始发射子弹
			Vector2 forward=RandomDirection();
			for (int i=0;i<total;i++)
				ShootBullet(transform.position,Rotate(forward,Mathf.PI*2*i/total),Random.Range(3f,5f),color);
			//发狂机制，时间或血量减少时BOSS移动速度加快
			float f=(cardtime-enemycs.stoptimecount)/cardtime;
			if ((float)enemycs.hp/enemycs.totalhp<f) f=(float)enemycs.hp/enemycs.totalhp;
			float t=Mathf.Log(f+1,2)/4+0.5f;
			enemycs.reach=false;
			enemycs.goalpos=RandomPosition(transform.position);
			enemycs.speed=Vector2.Distance(transform.position,enemycs.goalpos)/t;
		}
	}
	void Update(){
		Fire();
	}
}
