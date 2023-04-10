using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自机脚本

public class Player : MonoBehaviour{
	public GameObject Bullet;
	private GameObject checkpoint;
	private float xmin,xmax,ymin,ymax; //屏幕边界
	void Start(){
		xmin=global.xmin;xmax=global.xmax;
		ymin=global.ymin;ymax=global.ymax;
		checkpoint=transform.GetChild(0).gameObject;
		checkpoint.GetComponent<Renderer>().enabled=false;
		checkpoint.SetActive(true);
	}
	Vector3 LimitPosition(Vector3 pos){ //阻止角色移动到屏幕外
		float x=Mathf.Clamp(pos.x,xmin+0.1f,xmax-0.1f);
		float y=Mathf.Clamp(pos.y,ymin+0.1f,ymax-0.1f);
		return new Vector3(x,y,pos.z);
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
		Vector3 forward=Vector3.zero;
		if (Input.GetKey(KeyCode.LeftArrow)) forward.x=-1;
		else if (Input.GetKey(KeyCode.RightArrow)) forward.x=1;
		if (Input.GetKey(KeyCode.UpArrow)) forward.y=1;
		else if (Input.GetKey(KeyCode.DownArrow)) forward.y=-1;
		forward=forward.normalized;
		Vector3 pos=transform.position+forward*v;
		transform.position=LimitPosition(Vector3.Lerp(transform.position,pos,Time.deltaTime));
	}
	void ShootBullet(Vector3 pos,Vector3 forward){ //发射子弹
		GameObject bullet=Instantiate(Bullet,pos,Quaternion.identity);
		SpriteRenderer sr=bullet.GetComponent<SpriteRenderer>();
		sr.color=new Color(1,1,1,0.5f);
		Bullet bulletcs=bullet.GetComponent<Bullet>();
		bulletcs.speed=15;
		bulletcs.movetowards=forward;
		bulletcs.xmin=xmin;bulletcs.xmax=xmax;
		bulletcs.ymin=ymin;bulletcs.ymax=ymax;
		bulletcs.type=0;
	}
	private float timecount=0;
	private const float firedeltatime=0.05f; //子弹发射时间间隔
	void Fire(){ //发射子弹
		timecount+=Time.deltaTime;
		if (Input.GetKey(KeyCode.Z) && timecount>firedeltatime){
			ShootBullet(transform.position+Vector3.up*0.4f,Vector3.up);
			timecount=0;
		}
	}
	void Update(){
		MoveControl();
		Fire();
	}
}
