using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    private Dictionary<Item, float> availableLoots = new Dictionary<Item, float>();

    private void SaveItem(Item item, float[] rates)
    {
        float rate = item.Rarity switch
        {
            ItemRarity.normal => rates[0],
            ItemRarity.difficult => rates[1],
            ItemRarity.rare => rates[2],
            ItemRarity.epic => rates[3],
            _ => 0
        };
        availableLoots.Add(item, rate);
    }
    public List<Item> DrawThreeItems(List<Item> levelLoots, float[] rates)
    {
        availableLoots.Clear();
        foreach (var item in levelLoots)
        {
            SaveItem(item, rates);
        }
        List<Item> result = new List<Item>();

        Item item0 = RandomLootGenerator.Generate(availableLoots);
        availableLoots.Remove(item0);
        result.Add(item0);
        Item item1 = RandomLootGenerator.Generate(availableLoots);
        availableLoots.Remove(item1);
        result.Add(item1);
        Item item2 = RandomLootGenerator.Generate(availableLoots);
        availableLoots.Remove(item2);
        result.Add(item2);

        return result;
    }
}
