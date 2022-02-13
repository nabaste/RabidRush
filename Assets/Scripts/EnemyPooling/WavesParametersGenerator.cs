using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

public class WavesParametersGenerator : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    [SerializeField] private LevelData ld;
    [SerializeField] private ZombieList zl;
    private ZombiePool _zp;
    private float _budget;

    private List<ZombieSpawner> spawners = new List<ZombieSpawner>();

    private void Awake()
    {
        _zp = GetComponent<ZombiePool>();
        _budget = pd.Level switch
        {
            DifficultyLevel.Easy => ld.Budgets[0],
            DifficultyLevel.Medium => ld.Budgets[1],
            DifficultyLevel.Hard => ld.Budgets[2],
            _ => _budget
        };
        CalculateAmounts();

    }

    private void CalculateAmounts()
    {
        for (int i = 0; i < ld.StartLocations.Length; i++)
        {
            var locationBudget = _budget * ld.StartLocations[i].Priority;
            List<float> _maxEach = new List<float>();
            Dictionary<ZombieData, float> chances = new Dictionary<ZombieData, float>();
            for (int j = 0; j < zl.zombies.Count; j++)
            {
                var maxAmountOfThisKind = locationBudget / zl.zombies[j].PoolingCost;
                _maxEach.Add(maxAmountOfThisKind);

                //Cuantos grupos enteros de este tipo de zombies entran en el budget
                var groupAmount = (maxAmountOfThisKind / zl.zombies[j].Grouping) *
                                  ld.StartLocations[i].ZombieComposition[zl.zombies[j]];
                groupAmount = Mathf.Round(groupAmount);

                //Cuanto más rígido es el grupo, más chances tiene de salir un número cercano a su grupo.
                var chance = zl.zombies[j].GroupingVariance * (groupAmount % 1);
                chances.Add(zl.zombies[j], chance);

                var objectsPools = new ZombiePool.ObjectsPools(zl.zombies[j].Prefab.name + i,
                    zl.zombies[j].Grouping, CalculateTimes((int)groupAmount, j), zl.zombies[j].Prefab);
                _zp.pools.Add(objectsPools);


                // Debug.Log($"En la posición número {i}, salen {groupAmount} grupos de {zl.zombies[j].Prefab.name}. " +
                //           $"Cada grupo es de {zl.zombies[j].Grouping} miembros. " +
                //           $"Se gastó {zl.zombies[j].PoolingCost * zl.zombies[j].Grouping * groupAmount} del budget");
            }

            var extra = RandomLootGenerator.Roll(chances);
        }
        _zp.CreatePoolObjects();
        CreateSpawners();
    }

    private List<float> CalculateTimes(int numberOfWaves, int zombie)
    {
        var result = new List<float>();
        var sum = 0f;
        for (var i = 0; i < numberOfWaves; i++)
        {
            var time = UnityEngine.Random.Range(0, zl.zombies[zombie].OuterGroupingTimeSeparation);
            sum += time;
            result.Add(sum);
        }
        
        return result;
    }

    private void CreateSpawners()
    {
        for (int i = 0; i < ld.StartLocations.Length; i++)
        {
            foreach (var t in zl.zombies)
            {
                var spawnerObject = new GameObject
                {
                    transform =
                    {
                        parent = transform,
                        position = ld.StartLocations[i].Position
                    },
                    name = t.Prefab.name + i + "Spawner"
                };
                var spawner = spawnerObject.AddComponent<ZombieSpawner>();
                spawner.pool = GetComponent<ZombiePool>();
                spawner.spawnPosition = ld.StartLocations[i].Position;
                spawner.poolTag = t.Prefab.name + i;
                spawner.spacing = new WaitForSeconds(t.InnerGroupingTimeSeparation);
                spawner.groupSize = t.Grouping;
                
                spawners.Add(spawner);

                //Disable the spawner. It will be enabled when the player hits START
                spawner.enabled = false;
            }
        }
    }

    public void EnableSpawners()
    {
        foreach (var spawner in spawners)
        {
            spawner.enabled = true;
        }
    }

}