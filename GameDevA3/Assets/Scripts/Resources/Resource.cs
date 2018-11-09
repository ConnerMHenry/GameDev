using UnityEngine;
using System.Collections;

public class Resource : ScriptableObject
{
    public int Max { get; protected set; }
    public int Min { get; protected set; }
}
