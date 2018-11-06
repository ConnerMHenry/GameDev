using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGenerator {

    [System.Serializable]
    public struct BiomeResources {
        public Biome Biome;
        public List<TileObject> Resources;
    }

    public List<BiomeResources> Resources;

    public int RandomizeResourceAmount(int tileAmount, int paddingMin, int paddingMax) {
        return Random.Range(paddingMin, tileAmount - paddingMax);
    }

    public TileObject CreateResource(Biome tileBiome) {

        BiomeResources biomeResources = Resources.Find((res) => res.Biome == tileBiome);
        List<TileObject> resourceList = biomeResources.Resources;
        int index = Random.Range(0, resourceList.Count);

        return resourceList[index];
    }
}
