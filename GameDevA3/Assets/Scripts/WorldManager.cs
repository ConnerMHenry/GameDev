using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour {
	public bool testing;

	// Use this for initialization
	void Start () {
        GetComponent<WorldCreator>().Generate();
		testing = DebugModeManager.IsTesting;
	}
	
	// Update is called once per frame
	void Update () {
		if (testing)
		{
			foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType))) {
				ItemBarController.main.Add(type, 999);
			}

			testing = false;
		}
	}
}
