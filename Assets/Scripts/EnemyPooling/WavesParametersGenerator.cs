using System;
using System.Collections.Generic;
using RabidRush.ScriptableObjects;
using UnityEngine;

public class WavesParametersGenerator : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    [SerializeField] private LevelData ld;
    [SerializeField] private ZombieList zl;
    private ZombiePool _zp;
    private float _budget;

    private List<float> times = new List<float>();

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

    private void Start()
    {
        // _zp.CreatePoolObjects();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _zp.CreatePoolObjects();
            Make();
        }
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


                Debug.Log($"En la posición número {i}, salen {groupAmount} grupos de {zl.zombies[j].Prefab.name}. " +
                          $"Cada grupo es de {zl.zombies[j].Grouping} miembros. " +
                          $"Se gastó {zl.zombies[j].PoolingCost * zl.zombies[j].Grouping * groupAmount} del budget");
            }

            var extra = RandomLootGenerator.Roll(chances);
        }
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

    private void Make()
    {
        for (int i = 0; i < ld.StartLocations.Length; i++)
        {
            for (int j = 0; j < zl.zombies.Count; j++)
            {
                CreateSpawner(ld.StartLocations[i].Position, zl.zombies[j].Prefab.name + i, times);
            }
        }
    }

    private void CreateSpawner(Vector3 pos, string name, List<float> times)
    {
        var origin = new GameObject
        {
            transform =
            {
                parent = transform,
                position = pos
            },
            name = name
        };
        var spawner = origin.AddComponent<ZombieSpawner>();
        spawner.pool = GetComponent<ZombiePool>();
        spawner.spawnPosition = pos;
        spawner.poolTag = name;

        //Disable the spawner. It will be enabled when the player hits START
        spawner.enabled = false;
    }
}