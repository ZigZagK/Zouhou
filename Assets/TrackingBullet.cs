using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//诱导子弹

public class TrackingBullet : MonoBehaviour{
	public float xmin,xmax,ymin,ymax; //子弹边界
	public float speed; //飞行速度
	public Vector3 movetowards; //目标飞行方向
	private Transform target; //目标敌机
	private Vector3 forward; //当前飞行方向
	void FindNearestEnemy(){ //向着最近的敌方诱导
		GameObject[] enemies=GameObject.FindGameObjectsWithTag("Enemy");
		if (enemies.Length==0) {target=null;return;}
		float MIN=1e30f;
		foreach(GameObject enemy in enemies)
			if (Vector2.Distance(transform.position,enemy.transform.position)<MIN){
				MIN=Vector2.Distance(transform.position,enemy.transform.position);
				target=enemy.transform;
			}
	}
	void Start(){
		target=null;
		forward=movetowards;
	}
	/*
	在速度为speed，角速度为anglespeed的情况下，匀速圆周运动的半径R=speed/anglespeed
	设与敌机的距离为dis，当anglespeed>speed/dis时，会做朝着敌机的圆周运动
	*/
	void AngleTowards(){
		if (target==null) FindNearestEnemy();
		if (target==null) return;
		movetowards=(target.position-transform.position).normalized; //朝向敌机
		float anglespeed=(speed/Vector2.Distance(transform.position,target.position))*3; //飞行角速度（弧度）
		float angle=Vector2.Angle(forward,movetowards);
		float deltaangle=Time.deltaTime*anglespeed*Mathf.Rad2Deg;
		if (angle<=deltaangle) forward=movetowards;
		else forward=Vector3.Slerp(forward,movetowards,deltaangle/angle);
	}
	void MoveTowards(){
		transform.up=forward; //子弹朝着目标方向
		transform.position+=forward*speed*Time.deltaTime;
	}
	bool OutofScreen(float x,float y){ //是否飞出边界外
		return x<xmin || x>xmax || y<ymin || y>ymax;
	}
	void OufofScreenDestroyCheck(){ //飞出边界外，子弹销毁
		if (OutofScreen(transform.position.x,transform.position.y))
			Destroy(gameObject);
	}
	void Update(){
		AngleTowards();
		MoveTowards();
		OufofScreenDestroyCheck();
	}
}
