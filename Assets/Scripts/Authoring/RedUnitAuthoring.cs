using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RedUnitAuthoring : MonoBehaviour
{
    public class Baker : Baker<RedUnitAuthoring>
    {
        public override void Bake(RedUnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new RedUnitTag());
        }
    }
}


public struct RedUnitTag : IComponentData
{

}
