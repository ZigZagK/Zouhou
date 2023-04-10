using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//随机发射子弹 敌机脚本

public class RandomEnemy : MonoBehaviour{
	private Enemy enemycs;
	void Awake(){
		enemycs=gameObject.GetComponent<Enemy>();
	}
	float Forward(Vector3 a){ //a向量的朝向角度
		return Mathf.Atan2(a.y,a.x);
	}
	Vector3 RandomDirection(){ //随机角度发射
		float L=Forward(new Vector3(global.xmin,global.ymin,0)-transform.position);
		float R=Forward(new Vector3(global.xmax,global.ymin,0)-transform.position);
		float angle=Random.Range(L,R);
		return new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0);
	}
	private float firetimecount=0;
	private float firedeltatime=0f; //子弹发射时间间隔
	void ShootBullet(Vector3 pos,Vector3 forward){
		GameObject bullet=Instantiate(enemycs.Bullet,pos,Quaternion.identity);
		SpriteRenderer sr=bullet.GetComponent<SpriteRenderer>();
		sr.color=Color.green;
		Bullet bulletcs=bullet.GetComponent<Bullet>();
		bulletcs.speed=Random.Range(2f,5f);
		bulletcs.movetowards=forward;
		bulletcs.xmin=global.xmin;bulletcs.xmax=global.xmax;
		bulletcs.ymin=global.ymin;bulletcs.ymax=global.ymax;
		bulletcs.type=1;
	}
	void Fire(){ //发射子弹
		if (enemycs.reach){ //到达位置后开始发射子弹
			firetimecount+=Time.deltaTime;
			if (firetimecount>firedeltatime){
				ShootBullet(transform.position,RandomDirection());
				firetimecount=0;
				firedeltatime=Random.Range(0.5f,1f);
			}
		}
	}
	void Update(){
		Fire();
	}
}
