using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Resource {

    public void OnEnable()
    {
        Max = 4;
        Min = 2;
    }
}
