using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnAuthoring : MonoBehaviour
{
    public GameObject blueUnit;

    public GameObject redUnit;

    public int unitCount;

    public bool isTeamBlue;



    private class Baker : Baker<SpawnAuthoring>
    {
        public override void Bake(SpawnAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnData
            {
                blueUnit = GetEntity(authoring.blueUnit , TransformUsageFlags.Dynamic),
                redUnit = GetEntity(authoring.redUnit , TransformUsageFlags.Dynamic),
                unitCount = authoring.unitCount,
                isTeamBlue = authoring.isTeamBlue
            });
        }
    }

}

public struct SpawnData : IComponentData
{
    public Entity blueUnit;
    public Entity redUnit;
    public int unitCount;
    public bool isTeamBlue;
}
