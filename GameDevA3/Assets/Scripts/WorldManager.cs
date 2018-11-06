using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<WorldCreator>().Generate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
