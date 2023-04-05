using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

//符卡模式脚本

public class SpellCard : MonoBehaviour{
	public GameObject Enemy;
	private GameObject enemy;
	public GameObject TimeText;
	public GameObject HPBar;
	private TextMeshProUGUI Text;
	private RectTransform HP;
	private Enemy enemycs;
	private float cardtime; //符卡时间
	void Awake(){
		global.GlobalInfoInit();
		Text=TimeText.GetComponent<TextMeshProUGUI>();
		HP=HPBar.GetComponent<RectTransform>();
		global.bossname="Boss1"; //Debug
	}
	void Start(){ //创建Boss敌机
		enemy=Instantiate(Enemy,new Vector3(0,5,0),Quaternion.identity);
		enemy.AddComponent(Type.GetType(global.bossname));
		enemycs=enemy.GetComponent<Enemy>();
		cardtime=enemycs.stoptime;
	}
	void UpdateUI(){ //更新界面
		Text.text=Mathf.CeilToInt(cardtime).ToString();
		float offset=(global.xmax-global.xmin)*(enemycs.totalhp-enemycs.hp)/enemycs.totalhp*100;
		HP.offsetMax=new Vector2(-offset,0f);
	}
	void Update(){
		if (global.gameover) return; //游戏已经结束则不再继续判定
		if (global.score==1){ //成功击破敌机
			global.gameover=true;
			StartCoroutine(global.loadScene("GameOver"));
			return;
		}
		cardtime-=Time.deltaTime;
		if (cardtime<0){ //符卡时间内未能击破敌机
			global.gameover=true;
			global.miss++;
			StartCoroutine(global.loadScene("GameOver"));
			return;
		}
		global.survival_time+=Time.deltaTime;
		UpdateUI();
	}
}
