using UnityEngine;
using System.Collections;

public class Resource
{
    public ResourceType ResourceType;

    public Resource(ResourceType resource) {
        this.ResourceType = resource;

        switch(ResourceType) {
            case ResourceType.Wood:
                Max = 4;
                Min = 2;
                break;

            case ResourceType.Stone:
                Max = 1;
                Min = 3;
                break;

            case ResourceType.Iron:
                Min = 0;
                Max = 1;
                break;

            case ResourceType.Coal:
                Min = 0;
                Max = 1;
                break;

            case ResourceType.Crytsal:
                Max = 5;
                Min = 5;
                break;
        }
    }

    public int Max;
    public int Min;
    public int Amount {
        get {
            return Random.Range(Min, Max);
        }
    }
}
