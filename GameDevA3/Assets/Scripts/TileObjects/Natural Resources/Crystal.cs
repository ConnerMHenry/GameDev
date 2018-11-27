using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : NaturalResource
{
    public override void CreateResource()
    {
        PeopleRequired = 5;
        harvestTime = 15;

        float x = 0f;
        float y = 0f;

        x = Random.Range(xBoundary * -1, xBoundary) * boundaryMultiplier;
        y = Random.Range(yBoundary * -1, yBoundary) * boundaryMultiplier;

        GameObject spriteHolder = Instantiate(SpriteHolderPrefab);
        spriteHolder.GetComponent<SpriteRenderer>().sprite = spriteOptions[Random.Range(0, spriteOptions.Count)];

        // Set the local position to this gameobjects and set this object as parent
        spriteHolder.transform.parent = gameObject.transform;
        spriteHolder.transform.localPosition = transform.localPosition;

        // Move to generated position
        spriteHolder.transform.Translate(new Vector3(x, y));
    }
}
