using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log(GetComponent<Text>());
		LivingResourcesManager.personCount = GetComponent<Text>();
		LivingResourcesManager.AddLivingSpace(5);
		LivingResourcesManager.AddWorkers(5);
	}
}
