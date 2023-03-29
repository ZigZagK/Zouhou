using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自机被击中

public class Behit : MonoBehaviour{
	void OnTriggerEnter2D(Collider2D other){
		Destroy(gameObject);
		Destroy(transform.parent.gameObject);
	}
}
