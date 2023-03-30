using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour{
	AsyncOperation operation;
	IEnumerator loadScene(int index){ //异步加载场景
		operation=SceneManager.LoadSceneAsync(index,LoadSceneMode.Single);
		operation.allowSceneActivation=true;
		yield return operation;
	}
	public void StartGame(){
		StartCoroutine(loadScene(1));
	}
}
