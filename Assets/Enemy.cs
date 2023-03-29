using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌机脚本

public class Enemy : MonoBehaviour{
	public GameObject Bullet;
	public int hp=10; //血量
	public float speed=3; //飞行速度
	public Vector3 goalpos; //停止位置
	private bool reach=false,stop=false; //是否到达停止位置
	void MoveTowards(){ //朝着停止位置前进
		if (reach) return;
		Vector3 pos=transform.position+(goalpos-transform.position).normalized*speed;
		transform.position=Vector3.Lerp(transform.position,pos,Time.deltaTime);
		if (Vector3.Distance(transform.position,goalpos)<0.1f) reach=true;
	}
	float Forward(Vector3 a){ //a向量的朝向角度
		return Mathf.Atan2(a.y,a.x);
	}
	Vector3 RandomDirection(){ //随机角度发射
		float L=Forward(new Vector3(Game.xmin,Game.ymin,0)-transform.position);
		float R=Forward(new Vector3(Game.xmax,Game.ymin,0)-transform.position);
		float angle=Random.Range(L,R);
		return new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0);
	}
	private float firetimecount=0;
	private float firedeltatime=0f; //子弹发射时间间隔
	private float stoptimecount=0;
	private const float stoptime=5; //一定时间后敌机自行撤退
	void Fire(){ //发射子弹
		if (reach){ //到达位置后开始发射子弹
			firetimecount+=Time.deltaTime;
			if (firetimecount>firedeltatime){
				GameObject bullet=Instantiate(Bullet,transform.position+Vector3.down*0.5f,Quaternion.identity);
				SpriteRenderer sr=bullet.GetComponent<SpriteRenderer>();
				sr.color=Color.green;
				Bullet bulletcs=bullet.GetComponent<Bullet>();
				bulletcs.speed=Random.Range(2f,5f);
				bulletcs.movetowards=RandomDirection();
				bulletcs.xmin=Game.xmin;bulletcs.xmax=Game.xmax;
				bulletcs.ymin=Game.ymin;bulletcs.ymax=Game.ymax;
				bulletcs.type=1;
				firetimecount=0;
				firedeltatime=Random.Range(0.5f,1f);
			}
			stoptimecount+=Time.deltaTime;
			if (stoptimecount>stoptime){ //敌机开始撤退
				goalpos=new Vector3(transform.position.x,6,0);
				reach=false;
				stop=true;
			}
		}
	}
	void Update(){
		if (reach && stop) Destroy(gameObject); //敌机撤退完成，销毁敌机
		MoveTowards();
		Fire();
	}
	void OnTriggerEnter2D(Collider2D other){
		Bullet now=other.gameObject.GetComponent<Bullet>();
		if (now!=null && now.type==0){ //子弹为自机子弹才判定击中
			hp--;
			if (hp<=0) Destroy(gameObject);
		}
	}
}
