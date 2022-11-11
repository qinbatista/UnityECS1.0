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
    readonly RefRW<TargetPositionECSData> _tarGetPositionECSData;
    const float _maxDistance = 5;
    public void Move(float _deltaTime)
    {
        float3 direction = math.normalize(_tarGetPositionECSData.ValueRO._targetPositionValue - _transformAspect.Position);
        _transformAspect.Position += direction * _deltaTime * _ECSDataSpeed.ValueRO._speedValue;
    }
    public void TestReachTarget(RefRW<RandomECSData> randomComponent)
    {
        if (math.distance(_transformAspect.Position, _tarGetPositionECSData.ValueRO._targetPositionValue) < _maxDistance)
        {
            _tarGetPositionECSData.ValueRW._targetPositionValue = GetNewPosition(randomComponent);
            Debug.Log("new position:" + _tarGetPositionECSData.ValueRW._targetPositionValue);
        }
    }
    float3 GetNewPosition(RefRW<RandomECSData> randomComponent)
    {
        return new float3(randomComponent.ValueRW._random.NextFloat(0, 10), randomComponent.ValueRW._random.NextFloat(0, 10), randomComponent.ValueRW._random.NextFloat(0, 10));
    }
}
