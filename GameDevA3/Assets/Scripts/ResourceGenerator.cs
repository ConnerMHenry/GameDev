using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGenerator {

    [System.Serializable]
    public struct BiomeResources {
        public Biome Biome;
        public List<ResourceWeight> Resources;
    }

    [System.Serializable]
    public struct ResourceWeight {
        public TileObject Resource;
        public float Weight;
    }

    public List<BiomeResources> Resources;

    public int RandomizeResourceAmount(int tileAmount, int paddingMin, int paddingMax) {
        return Random.Range(paddingMin, tileAmount - paddingMax);
    }

    public TileObject CreateResource(Biome tileBiome) {

        BiomeResources biomeResources = Resources.Find((res) => res.Biome == tileBiome);
        List<ResourceWeight> resourceList = biomeResources.Resources;

        return SelectedWeightedResource(resourceList);
    }

    // Source: https://forum.unity.com/threads/making-random-range-generate-a-value-with-probability.336374/
    public TileObject SelectedWeightedResource(List<ResourceWeight> resources)
    {
        if (resources.Count == 0) throw new System.ArgumentException("At least one range must be included.");
        if (resources.Count == 1) return resources[0].Resource;

        float total = 0f;
        for (int i = 0; i < resources.Count; i++) total += resources[i].Weight;

        float r = Random.value;
        float s = 0f;

        int cnt = resources.Count - 1;
        for (int i = 0; i < cnt; i++)
        {
            s += resources[i].Weight / total;
            if (s >= r)
            {
                return resources[i].Resource;
            }
        }

        return resources[cnt].Resource;
    }
}
