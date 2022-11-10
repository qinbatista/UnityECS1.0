using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MoveAspect : IAspect
{
    readonly TransformAspect _transformAspect;
    readonly RefRO<SpeedECSData> _ECSDataSpeed;
    readonly RefRO<TargetPositionECSData> _tarGetPositionECSData;

    public void Move(float _deltaTime)
    {
            float3  direction = math.normalize(_tarGetPositionECSData.ValueRO._targetPositionValue - _transformAspect.Position);
            _transformAspect.Position += direction * _deltaTime*_ECSDataSpeed.ValueRO._speedValue;
    }
}
