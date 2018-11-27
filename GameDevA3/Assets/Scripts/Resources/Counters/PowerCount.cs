using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		LivingResourcesManager.powerText = GetComponent<Text>();
		LivingResourcesManager.AddPower(0);
	}
}
