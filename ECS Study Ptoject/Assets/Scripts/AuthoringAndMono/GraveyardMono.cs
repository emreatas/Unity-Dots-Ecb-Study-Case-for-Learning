using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class GraveyardMono : MonoBehaviour
{
    public float2 FieldDimensions;
    public int NumberTombstonesToSpawn;
    public GameObject TombstonePrefab;
    public uint RandomSeed;
    public GameObject ZombiePrefab;
    public float ZombieSpawnRate;
}
public class GraveyardBaker : Baker<GraveyardMono>
{
    public override void Bake(GraveyardMono authoring)
    {
        var graveyardEntitiy = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(graveyardEntitiy, new GraveyardProperties
        {
            FieldDimensions = authoring.FieldDimensions,
            NumberTombstonesToSpawn = authoring.NumberTombstonesToSpawn,
            TombstonePrefab = GetEntity(authoring.TombstonePrefab, TransformUsageFlags.Dynamic),
            ZombiePrefab = GetEntity(authoring.ZombiePrefab, TransformUsageFlags.Dynamic),
            ZombieSpawnRate = authoring.ZombieSpawnRate
        });

        AddComponent(graveyardEntitiy, new GraveyardRandom
        {
            Value = Random.CreateFromIndex(authoring.RandomSeed)
        });

        AddComponent<ZombieSpawnPoints>(graveyardEntitiy);
        AddComponent<ZombieSpawnerTime>(graveyardEntitiy);
    }
}
