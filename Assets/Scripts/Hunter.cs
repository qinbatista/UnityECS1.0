using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;

public class Hunter : MonoBehaviour
{
    Entity targetEntity;
    void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            targetEntity = GetRandomEntity();
        }
        if (targetEntity != Entity.Null)
        {
            Vector3 followPosition = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalToWorldTransform>(targetEntity).Value.Position;
            transform.position = followPosition;
        }
    }
    Entity GetRandomEntity()
    {
        EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(PlayerTagComponent));
        NativeArray<Entity> entities = entityQuery.ToEntityArray(Allocator.TempJob);
        if (entities.Length > 0)
        {
            int randomIndex = Random.Range(0, entities.Length);
            Entity randomEntity = entities[randomIndex];
            entities.Dispose();
            return randomEntity;
        }
        else
        {
            return Entity.Null;
        }
    }
}
