using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

public class LootManager : MonoBehaviour
{
    private Dictionary<Item, float> availableLoots = new Dictionary<Item, float>();
    [SerializeField] float normalRarityRate;
    [SerializeField] float difficultRarityRate;
    [SerializeField] float rareRarityRate;
    [SerializeField] float epicRarityRate;
    [SerializeField] private ItemList _existing;
    [SerializeField] private ItemList _inventory;
    public UnityAction OnItem0Selected;
    public UnityAction OnItem1Selected;
    public UnityAction OnItem2Selected;
    private void Awake()
    {
        // OnItem0Selected += () => SaveSelectedItem(LevelManager.Instance.lootOptions[0]);
        // OnItem1Selected += () => SaveSelectedItem(LevelManager.Instance.lootOptions[1]);
        // OnItem2Selected += () => SaveSelectedItem(LevelManager.Instance.lootOptions[2]);
    }
    private void SaveItem(Item item)
    {
        float rate = 0;
        switch (item.Rarity)
        {
            case ItemRarity.normal:
                rate = normalRarityRate;
                break;
            case ItemRarity.difficult:
                rate = difficultRarityRate;
                break;
            case ItemRarity.rare:
                rate = rareRarityRate;
                break;
            case ItemRarity.epic:
                rate = epicRarityRate;
                break;
        }
        availableLoots.Add(item, rate);
    }
    public List<Item> DrawThreeItems()
    {
        availableLoots.Clear();
        // foreach (var item in _existing.items)
        // {
        //     SaveItem(item);
        // }
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
    private void SaveSelectedItem(Item item)
    {
        // _inventory.items.Add(item);
    }
}
