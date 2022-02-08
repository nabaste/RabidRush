using System;
using UnityEngine;

    public interface IPoolable
    {
       event Action<string,GameObject> OnEliminateObject;
       string PoolTag { get; set; }
    }