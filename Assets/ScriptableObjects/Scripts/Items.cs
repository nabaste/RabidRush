using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item List", menuName = "ScriptableObjects/Items", order = 0)]
    public class Items : ScriptableObject
    {
        public List<Item> items = new List<Item>();
    }
}
