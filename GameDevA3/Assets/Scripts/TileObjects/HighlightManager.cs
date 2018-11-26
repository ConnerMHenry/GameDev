using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HighlightManager {

	public static Tile current_target;
	public static GameObject highlight;

	public static void Highlight(Tile target)
	{
		if (highlight == null)
		{
			Debug.Log("a");
			Object hex = Resources.Load("HexHighlighter");
			Debug.Log("b");
			Object obj = GameObject.Instantiate(hex);
			Debug.Log("c");
			highlight = (GameObject)obj;
		}

		if (current_target == null)
		{
			current_target = target;
        }
		else if (target == current_target && highlight.activeSelf)
		{
			highlight.SetActive(false);
            current_target.IsHighlighted = false;
            TileInfoController.main.CurrentTile = null;
			return;
		}

        current_target.IsHighlighted = false;
        target.IsHighlighted = true;
		current_target.terrainImage.sortingOrder = 0;
		current_target = target;
		current_target.terrainImage.sortingOrder = 2;
		highlight.transform.parent = target.gameObject.transform;
		highlight.transform.localPosition = Vector3.zero;
		highlight.SetActive(true);
        TileInfoController.main.CurrentTile = current_target;
    }
}
