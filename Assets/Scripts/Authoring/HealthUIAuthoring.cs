using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIAuthoring : MonoBehaviour
{
    public float buffer;
    public TextMeshProUGUI healthText;

    public class Baker : Baker<HealthUIAuthoring>
    {
        public override void Bake(HealthUIAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new HealthUIComponentData
            {
                buffer = authoring.buffer,
                healthText = GetEntity(authoring.healthText, TransformUsageFlags.Dynamic),
            });
        }
    }
}

public struct HealthUIComponentData: IComponentData
{
    public float buffer;
    public Entity healthText;
}