using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlightable : MonoBehaviour {

	private Tile tile;

	// Use this for initialization
	void Start () {
		tile = GetComponent<Tile>();
	}

	// Update is called once per frame
	private void OnMouseDown()
	{
		HighlightManager.Highlight(tile);
	}
}
