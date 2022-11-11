using Unity.Entities;
public partial class PlayerSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery playerEntityQuery = GetEntityQuery(typeof(PlayerTagComponent));

        PlayerSpawnerComponent playerSpawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>();
        RefRW<RandomComponent>  randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        int amount = 1;
        if (playerEntityQuery.CalculateEntityCount() < amount)
        {
            // EntityManager.Instantiate(playerSpawnerComponent.playerPrefab); //can't manage objects
            Entity spawnedEntity = entityCommandBuffer.Instantiate(playerSpawnerComponent.playerPrefab);
            entityCommandBuffer.SetComponent(spawnedEntity,
            new SpeedComponent
            {
                _speedValue = randomComponent.ValueRW._random.NextFloat(1, 5)
            });
        }
    }
}
