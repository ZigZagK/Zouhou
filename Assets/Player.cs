using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//自机脚本

public class Player : MonoBehaviour{
	public GameObject Subplane;
	private BulletSet bs;
	private GameObject[] sub;
	private Vector2[,] subpos={ //高低速子机位置
		{new Vector2(-1f,0.5f),new Vector2(1f,0.5f)},
		{new Vector2(-0.3f,1f),new Vector2(0.3f,1f)}
	};
	private GameObject checkpoint;
	private float xmin,xmax,ymin,ymax; //屏幕边界
	void Awake(){
		bs=gameObject.GetComponent<BulletSet>();
	}
	void Start(){
		xmin=global.xmin;xmax=global.xmax;
		ymin=global.ymin;ymax=global.ymax;
		checkpoint=transform.GetChild(0).gameObject;
		checkpoint.GetComponent<Renderer>().enabled=false;
		checkpoint.SetActive(true);
		sub=new GameObject[2];
		for (int i=0;i<2;i++){
			sub[i]=Instantiate(Subplane,transform);
			sub[i].AddComponent(Type.GetType(global.subname)); //使用对应类型子机
		}
		for (int i=0;i<2;i++) sub[i].transform.localPosition=subpos[0,i];
	}
	Vector2 LimitPosition(Vector2 pos){ //阻止角色移动到屏幕外
		float x=Mathf.Clamp(pos.x,xmin+0.1f,xmax-0.1f);
		float y=Mathf.Clamp(pos.y,ymin+0.1f,ymax-0.1f);
		return new Vector2(x,y);
	}
	private const float highspeed=6f; //高速移动速度
	private const float lowspeed=2f; //低速移动速度
	void MoveControl(){
		//按下shift出现判定点，并进入低速模式
		if (Input.GetKeyDown(KeyCode.LeftShift))
			checkpoint.GetComponent<Renderer>().enabled=true;
		if (Input.GetKeyUp(KeyCode.LeftShift))
			checkpoint.GetComponent<Renderer>().enabled=false;
		float v=highspeed;
		if (Input.GetKey(KeyCode.LeftShift)) v=lowspeed;
		//角色移动
		Vector3 forward=Vector2.zero;
		if (Input.GetKey(KeyCode.LeftArrow)) forward.x=-1;
		else if (Input.GetKey(KeyCode.RightArrow)) forward.x=1;
		if (Input.GetKey(KeyCode.UpArrow)) forward.y=1;
		else if (Input.GetKey(KeyCode.DownArrow)) forward.y=-1;
		forward=forward.normalized;
		transform.position=LimitPosition(transform.position+forward*v*Time.deltaTime);
	}
	void UpdateSubPlane(){ //更新子机状态
		int id=0;
		if (checkpoint.GetComponent<Renderer>().enabled) id=1;
		for (int i=0;i<2;i++) sub[i].GetComponent<Subplane>().goalpos=subpos[id,i];
	}
	void ShootBullet(Vector2 pos,Vector2 forward){ //发射直线子弹
		GameObject bullet=Instantiate(bs.Bullet,pos,Quaternion.identity);
		bullet.GetComponent<Bullet>().type=0;
		SpriteRenderer sr=bullet.GetComponent<SpriteRenderer>();
		sr.color=new Color(1,1,1,0.5f);
		StraightBullet bulletcs=bullet.AddComponent<StraightBullet>();
		bulletcs.speed=15;
		bulletcs.movetowards=forward;
		bulletcs.xmin=global.xmin;bulletcs.xmax=global.xmax;
		bulletcs.ymin=global.ymin;bulletcs.ymax=global.ymax;
	}
	private float firetimecount=0;
	private const float firedeltatime=0.05f; //子弹发射时间间隔
	void Fire(){ //发射子弹
		firetimecount+=Time.deltaTime;
		if (Input.GetKey(KeyCode.Z) && firetimecount>firedeltatime){
			ShootBullet(transform.position+Vector3.up*0.4f,Vector2.up);
			firetimecount=0;
		}
	}
	void Update(){
		MoveControl();
		UpdateSubPlane();
		Fire();
	}
}
