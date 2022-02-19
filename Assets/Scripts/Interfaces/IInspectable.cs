using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInspectable
{
    public string GetName();
    public float GetLife();
    public Dictionary<string, float> GetStats();
    public Transform GetTransform();
    public Dictionary<string, Action> GetPossibleActions();
}