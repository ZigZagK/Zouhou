using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//全局初始化

public class Initialization{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void SubsystemRegistration(){
		Screen.SetResolution(600,750,false);
	}
}
