using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceOptions {
    public List<ResourceSpriteBundle> resourceSprites;

    [System.Serializable]
    public struct ResourceSpriteBundle {
        public Sprite sprite;
        public ResourceType type;
    }

    public Sprite GetSprite(ResourceType type) {
        foreach (ResourceSpriteBundle bundle in resourceSprites) {
            if (bundle.type == type) {
                return bundle.sprite;
            }
        }

        return null;
    }

}
