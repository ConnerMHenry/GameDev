using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour {


    protected Tile parentTile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTile(Tile newtile)
    {
        this.parentTile = newtile;
        this.transform.localPosition = parentTile.transform.localPosition;
    }

    public void OnDestroy()
    {
        parentTile.ChildObject = null;
    }
}
