using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public partial class MovingSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        /*run in main thread*/
        foreach((TransformAspect transformAspect, RefRO<ECSDataSpeed> _ECSDataSpeed) in SystemAPI.Query<TransformAspect,RefRO<ECSDataSpeed>>())
        {
            transformAspect.Position += new float3(SystemAPI.Time.DeltaTime*_ECSDataSpeed.ValueRO.value, 0, 0);
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
