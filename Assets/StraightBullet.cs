using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//直线子弹脚本

public class StraightBullet : MonoBehaviour{
	public float xmin,xmax,ymin,ymax; //子弹边界
	public float speed; //飞行速度
	public float acce=0; //子弹加速度
	public Vector3 movetowards; //飞行方向
	void Start(){
		movetowards=movetowards.normalized; //方向单位化
		transform.up=movetowards; //子弹朝着目标方向
	}
	void MoveTowards(){
		transform.position+=movetowards*speed*Time.deltaTime;
	}
	bool OutofScreen(float x,float y){ //是否飞出边界外
		return x<xmin || x>xmax || y<ymin || y>ymax;
	}
	void OufofScreenDestroyCheck(){ //飞出边界外，子弹销毁
		if (OutofScreen(transform.position.x,transform.position.y))
			Destroy(gameObject);
	}
	void Update(){
		speed+=acce*Time.deltaTime;
		MoveTowards();
		OufofScreenDestroyCheck();
	}
}
