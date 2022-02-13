using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item List", menuName = "Scriptable Objects/Items/Item List", order = 5)]
    public class ItemList : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();
    }
}
