using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public partial class MovementSystem : SystemBase
{
    private GameManagerComponentData gameManger;

    protected override void OnStartRunning()
    {
        Entities.ForEach((in GameManagerComponentData gameManagerComponentData) =>
        {
            gameManger = gameManagerComponentData;
        }).WithoutBurst().Run();
    }



    protected override void OnUpdate()
    {
        float delta = SystemAPI.Time.DeltaTime;
        Entities.ForEach((Entity entity, ref LocalTransform translation, in UnitComponentData unitComponent, in FindTargetData targetFinder) =>
        {
                if (unitComponent.hp > 0)
                {
                    var targetEntity = targetFinder.targetGameObject;
                    if (targetEntity != Entity.Null && EntityManager.HasComponent<LocalTransform>(targetEntity))
                    {
                        var targetTranslation = EntityManager.GetComponentData<LocalTransform>(targetEntity);
                        var dist = math.distance(targetTranslation.Position, translation.Position);

                        if (dist > unitComponent.attackRange)
                        {
                            var direction = math.normalize(targetTranslation.Position - translation.Position);
                            translation.Position += direction * unitComponent.movementSpeed * delta;
                        }
                    }
                }
                else
                {
                    translation.Position = new float3(1000, 1000, 1000);
                }
            
        }).WithoutBurst().Run();
    }

}
