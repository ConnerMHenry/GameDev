using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Highlightable : MonoBehaviour {

	private Tile tile;

	// Use this for initialization
	void Start () {
		tile = GetComponent<Tile>();
	}

	// Update is called once per frame
	private void OnMouseDown()
	{
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //Do your thing.
            HighlightManager.Highlight(tile);
        }
	}
}
