using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public partial class MovingSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        foreach(TransformAspect transformAspect in SystemAPI.Query<TransformAspect>())
        {
            transformAspect.Position += new float3(SystemAPI.Time.DeltaTime, 0, 0);
        }
    }
}
