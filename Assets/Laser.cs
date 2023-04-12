using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour{
	public LineRenderer line;
	private BoxCollider2D bc;
	void Awake(){
		line=gameObject.GetComponent<LineRenderer>();
		bc=gameObject.GetComponent<BoxCollider2D>();
	}
	float Forward(Vector2 a){ //a向量的朝向角度
		return Mathf.Atan2(a.y,a.x);
	}
	void UpdateBox(Vector2 S,Vector2 T){
		bc.transform.eulerAngles=new Vector3(0,0,Forward(T-S)*Mathf.Rad2Deg);
		bc.transform.position=(S+T)/2f;
		bc.size=new Vector2(Vector2.Distance(S,T),line.startWidth);
	}
	public void SetLaser(Vector2 S,Vector2 T){
		line.SetPosition(0,S);
		line.SetPosition(1,T);
		UpdateBox(S,T);
	}
}
