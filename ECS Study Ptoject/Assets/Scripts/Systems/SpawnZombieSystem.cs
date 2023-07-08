using Mono.Cecil;
using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct SpawnZombieSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<ZombieSpawnerTime>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTÝme = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

        new SpawnZombieJob
        {
            DeltaTime = deltaTÝme,
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
        }.Run();
    }
}

[BurstCompile]
public partial struct SpawnZombieJob : IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer ECB;

    private void Execute(GraveyardAcpect graveyard)
    {
        graveyard.ZombieSpawnTimer -= DeltaTime;
        if (!graveyard.TimeToSpawnZombie) return;
        if (!graveyard.ZombieSpawnPointInitialized()) return;

        graveyard.ZombieSpawnTimer = graveyard.ZombieSpawnRate;
        var newZombie = ECB.Instantiate(graveyard.ZombiePrefab);

        var newZombieTransform = graveyard.GetZombieSpawnPoint();
        ECB.SetComponent(newZombie, newZombieTransform);

        var zombieHeading = MathHelpers.GetHeading(newZombieTransform.Position, graveyard.Position);
        ECB.SetComponent(newZombie, new ZombieHeading { Value = zombieHeading });
    }
}

