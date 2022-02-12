using System.Collections.Generic;
using UnityEngine;

namespace RabidRush.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item List", menuName = "ScriptableObjects/Items/Item List", order = 0)]
    public class ItemList : ScriptableObject
    {
        public List<Item> itemList = new List<Item>();
    }
}
