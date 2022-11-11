
using Unity.Entities;
using UnityEngine;

public class RandomAuthoring : MonoBehaviour
{

}
public class RandomBacker: Baker<RandomAuthoring>
{
    public override void Bake(RandomAuthoring authoring)
    {
        AddComponent(new RandomComponent
        {
            _random = new Unity.Mathematics.Random(1)
        });
    }
}