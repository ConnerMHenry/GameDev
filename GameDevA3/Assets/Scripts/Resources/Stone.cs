using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Resource
{

    public void OnEnable()
    {
        Max = 3;
        Min = 1;
    }
}
