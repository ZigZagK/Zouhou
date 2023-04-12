using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//激光子机脚本

public class LaserSub : MonoBehaviour{
	private BulletSet bs;
	private GameObject lasergo;
	private Laser laser;
	private Bullet bullet;
	void Awake(){
		bs=gameObject.GetComponent<BulletSet>();
		lasergo=Instantiate(bs.Laser);
		lasergo.SetActive(false);
		laser=lasergo.GetComponent<Laser>();
		laser.line.startColor=laser.line.endColor=new Color32(255,255,0,100);
		laser.line.startWidth=laser.line.endWidth=0.1f;
		bullet=lasergo.GetComponent<Bullet>();
		bullet.type=0;bullet.damage=7.5f;bullet.damagetype=1;
	}
	void Update(){
		if (Input.GetKeyDown(KeyCode.Z)) lasergo.SetActive(true);
		if (Input.GetKey(KeyCode.Z))
			laser.SetLaser(transform.position,new Vector2(transform.position.x,global.ymax));
		if (Input.GetKeyUp(KeyCode.Z)) lasergo.SetActive(false);
	}
}
