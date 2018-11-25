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
        this.transform.parent = newtile.transform;
        this.transform.localPosition = Vector3.zero;
    }

    public void OnDestroy()
    {
        if (parentTile.ChildObject == this)
        {
            parentTile.ChildObject = null;
        }

    }
}
