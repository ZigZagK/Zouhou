using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//全局信息

public class global{
	public static bool debug=false; //debug模式（不会死亡）
	public static float xmin,xmax,ymin,ymax; //屏幕边界
	public static int score=0; //击毁敌机数
	public static int miss=0; //未击毁敌机数
	public static float survival_time=0; //存活时间
	public static string bossname; //符卡模式下，Boss的名称
	public static int subtype=0; //子机种类名称
	public static string subname; //子机种类名称
	public static bool gameover=false; //游戏是否已经结束
	public static void GlobalInfoInit(){ //全局信息初始化
		//获取画面边界
		Vector2 leftbottom=Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 righttop=Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		xmax=righttop.x;
		xmin=leftbottom.x;
		ymax=righttop.y;
		ymin=leftbottom.y;
		//全局变量初始化
		miss=score=0;
		survival_time=0;
		gameover=false;
	}
	private static AsyncOperation operation;
	public static IEnumerator loadScene(string scenename){ //异步加载场景
		operation=SceneManager.LoadSceneAsync(scenename,LoadSceneMode.Single);
		operation.allowSceneActivation=true;
		yield return operation;
	}
}
