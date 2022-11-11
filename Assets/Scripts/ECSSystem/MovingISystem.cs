using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;

[BurstCompile]
public partial struct MovingISystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)//work as main thread
    {
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        float deltaTime = SystemAPI.Time.DeltaTime;
        JobHandle jobHandle = new MoveJob
        {
            deltaTime = deltaTime
        }.ScheduleParallel(state.Dependency);
        jobHandle.Complete();

        new TestReachedTargetPosition
        {
            randomComponent = randomComponent
        }.Run();

    }
}
[BurstCompile]
public partial struct MoveJob : IJobEntity
{
    public float deltaTime;
    public void Execute(MoveAspect _moveAspect)
    {
        _moveAspect.Move(deltaTime);
    }
}
[BurstCompile]
public partial struct TestReachedTargetPosition : IJobEntity
{
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;
    public void Execute(MoveAspect _moveAspect)
    {
        _moveAspect.TestReachTarget(randomComponent);
    }
}