using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//自机被击中

public class Behit : MonoBehaviour{
	AsyncOperation operation;
	IEnumerator loadScene(int index){ //异步加载场景
		operation=SceneManager.LoadSceneAsync(index,LoadSceneMode.Single);
		operation.allowSceneActivation=true;
		yield return operation;
	}
	void OnTriggerEnter2D(Collider2D other){ //被击中
		Destroy(gameObject);
		Destroy(transform.parent.gameObject);
		StartCoroutine(loadScene(2));
	}
}
