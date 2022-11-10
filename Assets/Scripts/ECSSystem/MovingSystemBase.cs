using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public partial class MovingSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        /*run in main thread*/
        foreach((   TransformAspect transformAspect,
                    RefRO<SpeedECSData> _ECSDataSpeed,
                    RefRO<TargetPositionECSData> _tarGetPositionECSData)

        in SystemAPI.Query<
                    TransformAspect,
                    RefRO<SpeedECSData>,
                    RefRO<TargetPositionECSData>>())
        {
            float3  direction = math.normalize(_tarGetPositionECSData.ValueRO._targetPositionValue - transformAspect.Position);
            transformAspect.Position += direction * SystemAPI.Time.DeltaTime*_ECSDataSpeed.ValueRO._speedValue;
        }

        /*run in different ways*/
        // Entities.ForEach((TransformAspect TransformAspect)=>
        // {
        //     TransformAspect.Position += new float3(SystemAPI.Time.DeltaTime, 0, 0);
        // }).ScheduleParallel(); //run multiple threads
        // }).Schedule(); //run on single work thread
        // }).Run(); //run on main thread
    }
}
