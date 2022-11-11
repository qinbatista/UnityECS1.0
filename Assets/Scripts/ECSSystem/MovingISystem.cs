using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;

public partial struct MovingISystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
    }

    public void OnDestroy(ref SystemState state)
    {
    }

    public void OnUpdate(ref SystemState state)//work as main thread
    {
        RefRW<RandomECSData> randomComponent = SystemAPI.GetSingletonRW<RandomECSData>();
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
public partial struct MoveJob : IJobEntity
{
    public float deltaTime;
    public void Execute(MoveAspect _moveAspect)
    {
        _moveAspect.Move(deltaTime);
    }
}

public partial struct TestReachedTargetPosition : IJobEntity
{
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomECSData> randomComponent;
    public void Execute(MoveAspect _moveAspect)
    {
        _moveAspect.TestReachTarget(randomComponent);
    }
}