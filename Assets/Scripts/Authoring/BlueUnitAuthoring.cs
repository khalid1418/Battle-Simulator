using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BlueUnitAuthoring : MonoBehaviour
{
    public class Baker : Baker<BlueUnitAuthoring>
    {
        public override void Bake(BlueUnitAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new BlueUnitTag());
        }
    }
}

public struct BlueUnitTag : IComponentData
{

}
