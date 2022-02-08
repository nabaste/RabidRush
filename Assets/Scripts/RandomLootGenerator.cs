using System.Collections;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;

public static class RandomLootGenerator 
{
    // public RandomLootGenerator()
    // {
        
    // }
    public static Item Generate(Dictionary<Item, float> dict)
    {
        float max = 0;

        foreach (var item in dict)
        {
            max += item.Value;
        }

        float random = Random.Range(0, max);

        foreach (var item in dict)
        {
            random -= item.Value;

            if(random <= 0)
            {
                return item.Key;
            }   
        }
        return default;
    }
    
    public static ZombieData Roll(Dictionary<ZombieData, float> dict)
    {
        float max = 0;

        foreach (var item in dict)
        {
            max += item.Value;
        }

        float random = Random.Range(0, max);

        foreach (var item in dict)
        {
            random -= item.Value;

            if(random <= 0)
            {
                return item.Key;
            }   
        }
        return default;
    }
}
