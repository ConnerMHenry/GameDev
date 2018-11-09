using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : NaturalResource<Wood> {

    private List<GameObject> treeAssets;

    public override void CreateResource()
    {
        resource = ScriptableObject.CreateInstance(typeof(Wood)) as Wood;
        treeAssets = new List<GameObject>();

        int treeAmount = 0;

        // Determine the amount of trees based on the resource size
        switch(Size) {
            // Small size only has one tree
            case ResourceSize.Small:
                treeAmount = 1;
                break;
            // Medium size only has two-three trees
            case ResourceSize.Medium:
                treeAmount = Random.Range(2, 4);
                break;

            // Big size only has four-five trees
            case ResourceSize.Big:
                treeAmount = Random.Range(4, 6);
                break;
            
            // Giant size can have 6-8 trees
            case ResourceSize.Giant:
                treeAmount = Random.Range(6, 9);
                break;

            default:
                break;
        }

        // Create the trees
        for (int i = 0; i < treeAmount; i++) {
            // Generate position within bounds
            float x = Random.Range(xBoundary * -1, xBoundary);
            float y = Random.Range(yBoundary * -1, yBoundary);

            // Create SpriteHolder and assign the sprite
            GameObject spriteHolder = Instantiate(SpriteHolderPrefab);
            spriteHolder.GetComponent<SpriteRenderer>().sprite = GenerateSprite();
            // Set the local position to this gameobjects and set this object as parent
            spriteHolder.transform.parent = gameObject.transform;
            spriteHolder.transform.localPosition = transform.localPosition;
            // Move to generated position
            spriteHolder.transform.Translate(new Vector3(x, y));

            treeAssets.Add(spriteHolder);
        }

        MinAmount = resource.Min * treeAmount;
        MaxAmount = resource.Max * treeAmount;
    } 
}
