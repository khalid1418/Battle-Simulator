using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


public partial class UiHealthBarSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        Entities.ForEach((ref HealthUIComponentData healthUIComponentData, in UnitComponentData unitComponentData, in LocalTransform translation) =>
        {
            if (unitComponentData.hp > 0)
            {
                if (healthUIComponentData.healthText == Entity.Null)
                {
                    Debug.LogWarning("Health text entity is null.");
                    return;
                }

                // Retrieve the GameObject corresponding to the Canvas
                GameObject canvas = GameObject.Find($"Canvas-{unitComponentData.unitNum}");
                if (canvas == null)
                {
                    Debug.LogError("Canvas not found.");
                    return;
                }

                // Retrieve the TextMeshProUGUI component from the Canvas's children
                TextMeshProUGUI healthTextMeshPro = canvas.GetComponentInChildren<TextMeshProUGUI>();
                if (healthTextMeshPro == null)
                {
                    Debug.LogError("TextMeshProUGUI component not found in Canvas children.");
                    return;
                }

                // Set the text and position
                healthTextMeshPro.text = unitComponentData.hp.ToString();
                healthTextMeshPro.rectTransform.position = new Vector3(translation.Position.x, translation.Position.y + healthUIComponentData.buffer, translation.Position.z);
            }
            else
            {
                GameObject canvas = GameObject.Find($"Canvas-{unitComponentData.unitNum}");
                TextMeshProUGUI healthTextMeshPro = canvas.GetComponentInChildren<TextMeshProUGUI>();
                healthTextMeshPro.enabled = false;

                if (healthUIComponentData.healthText != Entity.Null)
                {
                    // Disable the health text if the unit is dead
                    healthUIComponentData.healthText = Entity.Null;
                }
            }
        }).WithoutBurst().Run();
    }
}
