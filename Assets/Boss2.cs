using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//符卡2 高低速自机狙 敌机脚本

public class Boss2 : MonoBehaviour{
	private Enemy enemycs;
	private BulletSet bs;
	private GameObject player;
	private float cardtime=60; //符卡时间
	void Awake(){
		enemycs=gameObject.GetComponent<Enemy>();
		transform.position=new Vector2(0,5);
		enemycs.goalpos=new Vector2(0,2);
		enemycs.totalhp=enemycs.hp=300;
		enemycs.stoptime=cardtime;
		bs=gameObject.GetComponent<BulletSet>();
	}
	void Start(){
		player=GameObject.FindWithTag("Player");
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
	float Forward(Vector2 a){ //a向量的朝向角度
		return Mathf.Atan2(a.y,a.x);
	}
	private Color color1=new Color32(104,212,255,255);
	private Color color2=new Color32(255,175,7,255);
	private Color color3=Color.green;
	private float timecount=0;
	private float deltatime=0.1f;
	void Fire(){ //发射子弹
		if (player==null) return; //玩家已消失则停止发射子弹
		if (enemycs.reach){ //到达位置后开始发射子弹
			timecount+=Time.deltaTime;
			if (timecount>deltatime){
				float angle=Forward(player.transform.position-transform.position);
				Vector2 forward=new Vector2(Mathf.Cos(angle),Mathf.Sin(angle));
				for (int i=0;i<16;i++){
					Vector2 nowforward=Rotate(forward,Mathf.PI*i/8);
					float speed=Random.Range(2f,8f);
					Color nowcolor=(speed<=3?color1:(speed<=5?color2:color3));
					ShootBullet(transform.position,nowforward,speed,nowcolor);
				}
				timecount=0;
			}
		}
	}
	void Update(){
		Fire();
	}
}
