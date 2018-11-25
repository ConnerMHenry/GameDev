using UnityEngine;
using System.Collections;

public class Resource
{
    public ResourceType ResourceType { get; protected set; }

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

            case ResourceType.Crytsal:
                Max = 5;
                Min = 5;
                break;
        }
    }

    public int Max { get; protected set; }
    public int Min { get; protected set; }
}
