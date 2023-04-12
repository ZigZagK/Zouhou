using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子机脚本

public class Subplane : MonoBehaviour{
	public float speed=10;
	public Vector3 goalpos; //停止位置
	void MoveTowards(){ //朝着停止位置前进
		if (Vector2.Distance(transform.localPosition,goalpos)<=Time.deltaTime*speed){
			transform.localPosition=goalpos;
			return;
		}
		transform.localPosition+=(goalpos-transform.localPosition).normalized*speed*Time.deltaTime;
	}
	void Update(){
		MoveTowards();
	}
}
