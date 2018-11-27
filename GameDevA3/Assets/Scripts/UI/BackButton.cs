using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {
	public GameObject BackTo;
	public GameObject BackFrom;
	// Use this for initialization
	public void Go()
	{
		BackTo.SetActive(true);
		BackFrom.SetActive(false);
	}
}
