using Unity.Entities;
using UnityEngine;

public class SpeedAuthoring : MonoBehaviour
{
    public float _speedValue;
}
public class SpeedBacker : Baker<SpeedAuthoring>
{
    public override void Bake(SpeedAuthoring authoring)
    {
        AddComponent(new SpeedComponent{_speedValue = authoring._speedValue});
    }
}
