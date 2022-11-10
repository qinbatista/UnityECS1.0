using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpeedAuthoring : MonoBehaviour
{
    public float value;
}
public class SpeedBacker : Baker<SpeedAuthoring>
{
    public override void Bake(SpeedAuthoring authoring)
    {
        AddComponent(new ECSDataSpeed{value = authoring.value});
    }
}
