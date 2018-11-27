using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileObject : MonoBehaviour {


    public string Name;
    protected Tile parentTile;

    public int PeopleRequired { get; protected set; }
    protected int peopleWorking = 0;
    public abstract int PeopleWorking { get; set; }
    public abstract List<Resource> Resources { get; }

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
