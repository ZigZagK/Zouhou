using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自机移动脚本

public class Player : MonoBehaviour{
	public GameObject Bullet;
	private GameObject checkpoint;
	private float xmin,xmax,ymin,ymax; //屏幕边界
	void Start(){
		xmin=Game.xmin;xmax=Game.xmax;
		ymin=Game.ymin;ymax=Game.ymax;
		checkpoint=transform.GetChild(0).gameObject;
		checkpoint.GetComponent<Renderer>().enabled=false;
		checkpoint.SetActive(true);
	}
	Vector3 LimitPosition(Vector3 pos){ //阻止角色移动到屏幕外
		float x=Mathf.Clamp(pos.x,xmin,xmax);
		float y=Mathf.Clamp(pos.y,ymin,ymax);
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
		if (Input.GetKey(KeyCode.LeftArrow)){
			Vector3 pos=transform.position+Vector3.left*v;
			transform.position=LimitPosition(Vector3.Lerp(transform.position,pos,Time.deltaTime));
		} else if (Input.GetKey(KeyCode.RightArrow)){
			Vector3 pos=transform.position+Vector3.right*v;
			transform.position=LimitPosition(Vector3.Lerp(transform.position,pos,Time.deltaTime));
		}
		if (Input.GetKey(KeyCode.UpArrow)){
			Vector3 pos=transform.position+Vector3.up*v;
			transform.position=LimitPosition(Vector3.Lerp(transform.position,pos,Time.deltaTime));
		} else if (Input.GetKey(KeyCode.DownArrow)){
			Vector3 pos=transform.position+Vector3.down*v;
			transform.position=LimitPosition(Vector3.Lerp(transform.position,pos,Time.deltaTime));
		}
	}
	private float timecount=0;
	private const float firedeltatime=0.05f; //子弹发射时间间隔
	void Fire(){ //发射子弹
		timecount+=Time.deltaTime;
		if (Input.GetKey(KeyCode.Z) && timecount>firedeltatime){
			GameObject bullet=Instantiate(Bullet,transform.position+Vector3.up*0.4f,Quaternion.identity);
			Bullet bulletcs=bullet.GetComponent<Bullet>();
			bulletcs.speed=15;
			bulletcs.movetowards=Vector3.up;
			bulletcs.xmin=xmin;bulletcs.xmax=xmax;
			bulletcs.ymin=ymin;bulletcs.ymax=ymax;
			bulletcs.type=0;
			timecount=0;
		}
	}
	void Update(){
		MoveControl();
		Fire();
	}
}
