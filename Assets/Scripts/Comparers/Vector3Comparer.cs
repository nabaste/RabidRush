using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Comparer : Comparer<Vector3>
{
    public override int Compare(Vector3 x, Vector3 y)
    {
        return x.magnitude > y.magnitude ? 1 : -1; 
    }
}
