using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : NaturalResource {

    public override void CreateResource()
    {
        harvestTime = 3;

        Resource resource = resources.Find((obj) => obj.ResourceType == ResourceType.Stone);

        int rockIndex = 0;
        switch (Size)
        {
            case ResourceSize.Small:
                rockIndex = Random.Range(0, 4);
                Name = "Small Stone";
                PeopleRequired = 1;
                break;

            case ResourceSize.Medium:
                rockIndex = Random.Range(4, 6);
                resource.Min *= 3;
                resource.Min *= 3;
                harvestTime *= 1.5f;

                Name = "Sizable Rock";
                PeopleRequired = 4;
                break;

            case ResourceSize.Big:
                rockIndex = Random.Range(6, 8);
                resource.Min *= 6;
                resource.Min *= 6;
                harvestTime *= 3.0f;

                Name = "Boulder";
                PeopleRequired = 6;
                break;

            case ResourceSize.Giant:
                rockIndex = spriteOptions.Count - 1;
                resource.Min *= 9;
                resource.Min *= 9;
                harvestTime *= 5f;

                Name = "Mountain";
                PeopleRequired = 8;
                break;

            default:
                break;
        }

		resource.ReCalculateAmount();

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
