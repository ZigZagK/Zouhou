using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹脚本

public class Bullet : MonoBehaviour{
	public float xmin,xmax,ymin,ymax;
	public float speed; //飞行速度
	public Vector3 movetowards; //飞行方向
	public int type; //子弹类型，0为自机，1为敌机
	float Forward(Vector3 a){ //a向量的朝向角度
		return Mathf.Atan2(a.y,a.x);
	}
	void Start(){
		movetowards=movetowards.normalized; //方向单位化
		transform.Rotate(Vector3.forward,Forward(movetowards)*Mathf.Rad2Deg-90); //子弹朝着目标方向
	}
	void MoveTowards(){
		Vector3 pos=transform.position+movetowards*speed;
		transform.position=Vector3.Lerp(transform.position,pos,Time.deltaTime);
	}
	bool OutofScreen(float x,float y){ //是否飞出屏幕外
		return x<xmin || x>xmax || y<ymin || y>ymax;
	}
	void OufofScreenDestroyCheck(){ //飞出屏幕外，及时销毁
		if (OutofScreen(transform.position.x,transform.position.y))
			Destroy(gameObject);
	}
	void Update(){
		MoveTowards();
		OufofScreenDestroyCheck();
	}
	void OnTriggerEnter2D(Collider2D other){
		if (type==0){ //与敌机相撞
			Enemy now=other.gameObject.GetComponent<Enemy>();
			if (now!=null) Destroy(gameObject);
		} else { //与自机相撞
			Player now=other.gameObject.GetComponent<Player>();
			if (now!=null) Destroy(gameObject);
		}
	}
}
