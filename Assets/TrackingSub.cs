using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//诱导子机脚本

public class TrackingSub : MonoBehaviour{
	private BulletSet bs;
	void Awake(){
		bs=gameObject.GetComponent<BulletSet>();
	}
	void ShootBullet(Vector2 pos,Vector2 forward){ //发射诱导子弹
		GameObject bullet=Instantiate(bs.Bullet,pos,Quaternion.identity);
		Bullet value=bullet.GetComponent<Bullet>();
		value.type=0;value.damage=0.2f;
		SpriteRenderer sr=bullet.GetComponent<SpriteRenderer>();
		sr.color=new Color32(233,30,99,200);
		TrackingBullet bulletcs=bullet.AddComponent<TrackingBullet>();
		bulletcs.speed=5;
		bulletcs.movetowards=forward;
		bulletcs.xmin=global.xmin;bulletcs.xmax=global.xmax;
		bulletcs.ymin=global.ymin;bulletcs.ymax=global.ymax;
	}
	private float firetimecount=0;
	private const float firedeltatime=0.1f; //子弹发射时间间隔
	void Update(){
		firetimecount+=Time.deltaTime;
		if (Input.GetKey(KeyCode.Z) && firetimecount>firedeltatime){
			ShootBullet(transform.position,Vector2.up);
			firetimecount=0;
		}
	}
}
