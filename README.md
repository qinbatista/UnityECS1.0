# ECS1.0
## Component
###### Data ccomponent

``` C#
using Unity.Entities;
public struct YourComponent: IComponentData
{
	public float _float;
	public float3 _float3;
}

```


## System

#### SystemBase
###### Easier but can't use burst compiler


- **Foreach** Run in main Thread

```C#
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public partial class YourSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
		foreach (YourAspect _yourAspect in SystemAPI.Query<YourAspect>())
		{
		    RefRW<YourComponent> _yourComponent = SystemAPI.GetSingletonRW<YourComponent>();
		    _yourAspect.YourMethods(SystemAPI.Time.DeltaTime);
		}
    }
}

```
- **Entities.ForEach** Can be run in main Thread or multiple threads

```C#
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public partial class YourSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
		Entities.ForEach((TransformAspect TransformAspect)=>
		{
		    TransformAspect.Position += new float3(SystemAPI.Time.DeltaTime, 0, 0);
			}).ScheduleParallel(); 	//run multiple threads
			//}).Schedule(); 		//run on single work thread
			//}).Run(); 				//run on main thread
		}
	}
}
        
```
#### ISystem
###### Can use burst compiler and faster

```C#

using Unity.Entities;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;

[BurstCompile]
public partial struct YourISystem : ISystem
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
        RefRW<YourComponent> _yourComponent = SystemAPI.GetSingletonRW<yourComponent>();
        float deltaTime = SystemAPI.Time.DeltaTime;
        JobHandle jobHandle = new YourJob
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
public partial struct YourJob : IJobEntity
{
    public float deltaTime;
    public void Execute(MoveAspect _moveAspect)
    {
        _moveAspect.YourMethod(deltaTime);
    }
}

```
### Aspect
###### Simplify your code
```C#
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct YourAspect : IAspect
{
    readonly TransformAspect _transformAspect;
    readonly RefRO<YourComponent1> _yourComponent1;
    readonly RefRW<YourComponent2> _yourComponent2;
    const float _maxDistance = 5;
    public void Move(float _deltaTime)
    {
        float3 direction = math.normalize(_yourComponent2.ValueRO._yoruValue - _transformAspect.Position);
        _transformAspect.Position += direction * _deltaTime * _yourComponent2.ValueRO. _yoruValue;
    }
    public void YourMethod1(RefRW<YourComponent1> yourComponent1)
    {
        if (math.distance(_transformAspect.Position, YourComponent1.ValueRO. _yoruValue) < _maxDistance)
        {
            _yourComponent2.ValueRW._yoruValue = GetNewPosition(randomComponent);
            Debug.Log("new position:" + _yourComponent2.ValueRW._yoruValue);
        }
    }
    float3 YourMethod2(RefRW<YourComponent2> yourComponent2)
    {
	        return new float3(randomComponent.ValueRW._random.NextFloat(0, 10),yourComponent2.ValueRW._random.NextFloat(0, 10), yourComponent2.ValueRW._random.NextFloat(0, 10));
    }
}

```

### Baker
###### Attach DOTS on GameObject

```C#
using Unity.Entities;
using UnityEngine;

public class YourAuthoring : MonoBehaviour
{
	public float3 _yourGameObjectValue;
}
public class YourBaker: Baker<YourAuthoring>
{
    public override void Bake(YourAuthoring authoring)
    {
        AddComponent(new YourComponent
        {
            _yourComponentValue = _yourGameObjectValue
        });
    }
}


```

