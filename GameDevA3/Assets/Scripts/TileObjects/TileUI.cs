using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileUI : MonoBehaviour {

    public Tile parentTile;

    //private bool isFocused = false;

    //public bool IsFocused {
    //    get {
    //        return isFocused;
    //    }
    //    set {
    //        //gameObject.GetComponent<>
    //        isFocused = value;
    //    }
    //}

    public void Awake()
    {
        SetCanvas();
    }

    // Use this for initialization
    void Start () {

        if (this.transform.parent == null) 
        {
            SetCanvas();
        }

        SetPosition(parentTile);
	}

    private void SetCanvas() {
        GameObject parent = GameObject.FindGameObjectWithTag("UIWorldSpace");
        if (parent != null)
        {
            this.transform.parent = parent.transform;
        }
    }

    public void OnValidate()
    {
        SetPosition(parentTile);
    }

    public void SetPosition(Tile tile) {
        if (tile != null)
        {
            Vector3 tilePos = tile.transform.position;
            tilePos.y -= 0.5f;
            this.transform.position = tilePos;

            if (tile != parentTile)
            {
                parentTile = tile;
            }
        }
    }
}
