using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

public class LootManager : MonoBehaviour
{
    private Dictionary<Item, float> availableLoots = new Dictionary<Item, float>();
    public UnityAction OnItem0Selected;
    public UnityAction OnItem1Selected;
    public UnityAction OnItem2Selected;
    private void Awake()
    {
        // OnItem0Selected += () => SaveSelectedItem(LevelManager.Instance.lootOptions[0]);
        // OnItem1Selected += () => SaveSelectedItem(LevelManager.Instance.lootOptions[1]);
        // OnItem2Selected += () => SaveSelectedItem(LevelManager.Instance.lootOptions[2]);
    }
    private void SaveItem(Item item, float[] rates)
    {
        float rate = 0;
        switch (item.Rarity)
        {
            case ItemRarity.normal:
                rate = rates[0];
                break;
            case ItemRarity.difficult:
                rate = rates[1];
                break;
            case ItemRarity.rare:
                rate = rates[2];
                break;
            case ItemRarity.epic:
                rate = rates[3];
                break;
        }
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

        Time.timeScale = 0;

        return result;
    }
}
