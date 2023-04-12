using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//符卡4 跟着转 敌机脚本

public class Boss4 : MonoBehaviour{
	private Enemy enemycs;
	private BulletSet bs;
	private Laser[] laser;
	private float cardtime=60; //符卡时间
	private Color color1=new Color32(104,212,255,200);
	private float laserlen;
	private float lasermaxlen;
	void Awake(){
		enemycs=gameObject.GetComponent<Enemy>();
		transform.position=new Vector2(0,5);
		enemycs.speed=5;
		enemycs.goalpos=new Vector2(0,0);
		enemycs.totalhp=enemycs.hp=200;
		enemycs.stoptime=cardtime;
		bs=gameObject.GetComponent<BulletSet>();
		laser=new Laser[4];
		for (int i=0;i<4;i++){
			GameObject go=Instantiate(bs.Laser);
			laser[i]=go.GetComponent<Laser>();
			laser[i].line.startColor=laser[i].line.endColor=color1;
		}
		lasermaxlen=Mathf.Sqrt(global.xmax*global.xmax+global.ymax*global.ymax);
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
	Vector2 RandomDirection(){ //随机角度发射
		float angle=Random.Range(0,Mathf.PI*2);
		return new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
	}
	private Color color2=new Color32(255,175,7,255);
	private float bullettimecount=0;
	private float bulletdeltatime=0f;
	private int total=15; //弹幕将圆等分为多少部分
	private Vector2 laserforward=Vector2.up;
	private float laserspeed=1f;
	void Fire(){ //发射子弹
		if (enemycs.reach){ //到达位置后开始发射子弹
			float f=(cardtime-enemycs.stoptimecount)/cardtime;
			if ((float)enemycs.hp/enemycs.totalhp<f) f=(float)enemycs.hp/enemycs.totalhp;
			bullettimecount+=Time.deltaTime;
			if (bullettimecount>bulletdeltatime){
				Vector2 forward=RandomDirection();
				for (int i=0;i<total;i++)
					ShootBullet(transform.position,Rotate(forward,Mathf.PI*2*i/total),3,color2);
				bullettimecount=0;
				//发狂机制，时间或血量减少时弹幕发射间隔减小
				bulletdeltatime=Mathf.Log(f+1,2)/2+1f;
			}
			if (laserlen<lasermaxlen) laserlen+=Time.deltaTime*5;
			for (int i=0;i<4;i++){
				Vector3 forward=Rotate(laserforward,Mathf.PI*i/2);
				laser[i].SetLaser(transform.position,transform.position+forward*laserlen);
			}
			//发狂机制，时间或血量减少时激光转速加快
			laserspeed=Mathf.Log(1-f+1,2)/4+1f;
			laserforward=Rotate(laserforward,Time.deltaTime*laserspeed);
		}
	}
	void Update(){
		Fire();
	}
}
