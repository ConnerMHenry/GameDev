using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class DebugPlayButton : MonoBehaviour
{

	public void Play()
	{
		DebugModeManager.IsTesting = true;
		SceneManager.LoadScene("SampleScene");
	}
}
