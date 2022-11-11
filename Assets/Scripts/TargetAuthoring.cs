using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TargetAuthoring : MonoBehaviour
{
    public float3 _targetPositionValue;
}
public class TargetPositionBacker : Baker<TargetAuthoring>
{
    public override void Bake(TargetAuthoring authoring)
    {
        AddComponent(new TargetPositionComponent { _targetPositionValue = authoring._targetPositionValue });
    }
}