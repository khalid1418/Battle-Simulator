using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class FindTargetAuthoring : MonoBehaviour
{

    public GameObject target;

    public class Baker : Baker<FindTargetAuthoring>
    {
        public override void Bake(FindTargetAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new FindTargetData
            {
                targetGameObject = GetEntity(authoring.target, TransformUsageFlags.Dynamic),
            });

        }
    }
}


public struct FindTargetData:IComponentData
{
    public Entity targetGameObject;

}
