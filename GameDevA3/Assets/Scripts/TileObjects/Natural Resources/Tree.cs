﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : NaturalResource<Wood> {

    private List<GameObject> treeAssets;

    public override void CreateResource()
    {
        List<int> xCoordinates = new List<int>();
        List<int> yCoordinates = new List<int>();

        // Create list of possible x axis values
        for (int x = xBoundary * -1; x <= xBoundary; x++) { 
            xCoordinates.Add(x); 
        }
        // Create list of possible y axis values
        for (int y = yBoundary * -1; y <= yBoundary; y++) {
            yCoordinates.Add(y);
        }

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
            int xPos = xCoordinates[Random.Range(0, xCoordinates.Count)];
            int yPos = yCoordinates[Random.Range(0, yCoordinates.Count)];

            // Remove positions from list so they can't be re-used. Helps with spacing trees
            xCoordinates.Remove(xPos);
            yCoordinates.Remove(yPos);

            // Multiply the int positions so that they fit within the scale of -0.30 to 0.30 for both x and y axis
            float x = ((float)xPos) * boundaryMultiplier;
            float y = ((float)yPos) * boundaryMultiplier;

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

        LayerSprites();
    } 

    private void LayerSprites() {
        treeAssets.Sort((GameObject treeOne, GameObject treeTwo) =>
        {
            return treeTwo.transform.position.y.CompareTo(treeOne.transform.position.y);
        });

        for (int i = 0; i < treeAssets.Count; i++) {
            treeAssets[i].GetComponent<SpriteRenderer>().sortingOrder = i;
        }
    }
}
