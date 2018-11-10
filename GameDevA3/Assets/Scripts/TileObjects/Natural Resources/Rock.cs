using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : NaturalResource<Stone> {

    public override void CreateResource()
    {
        resource = ScriptableObject.CreateInstance(typeof(Stone)) as Stone;
        MinAmount = resource.Min;
        MaxAmount = resource.Max;

        int rockIndex = 0;
        switch (Size)
        {
            case ResourceSize.Small:
                rockIndex = Random.Range(0, 4);
                break;

            case ResourceSize.Medium:
                rockIndex = Random.Range(4, 6);
                MinAmount *= 3;
                MaxAmount *= 3;
                break;

            case ResourceSize.Big:
                rockIndex = Random.Range(6, 8);
                MinAmount *= 6;
                MaxAmount *= 6;
                break;

            case ResourceSize.Giant:
                rockIndex = spriteOptions.Count - 1;
                MinAmount *= 9;
                MaxAmount *= 9;
                break;

            default:
                break;
        }

        float x = 0f;
        float y = 0f;


            x = Random.Range(xBoundary * -1, xBoundary) * boundaryMultiplier;
            y = Random.Range(yBoundary * -1, yBoundary) * boundaryMultiplier;

        if (Size == ResourceSize.Giant)
        {
            x /= 3;
            y /= 3;
        }

        GameObject spriteHolder = Instantiate(SpriteHolderPrefab);
        spriteHolder.GetComponent<SpriteRenderer>().sprite = spriteOptions[rockIndex];

        // Set the local position to this gameobjects and set this object as parent
        spriteHolder.transform.parent = gameObject.transform;
        spriteHolder.transform.localPosition = transform.localPosition;

        // Move to generated position
        spriteHolder.transform.Translate(new Vector3(x, y));
    }
}
