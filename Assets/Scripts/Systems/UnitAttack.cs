using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

[BurstCompile]
public partial class UnitAttack : SystemBase
{
    private GameManagerComponentData gameManager;

    protected override void OnStartRunning()
    {
        Entities.ForEach((in GameManagerComponentData gameManagerComponentData) =>
        {
            gameManager = gameManagerComponentData;
        }).WithoutBurst().Run();
    }
    protected override void OnUpdate()
    {
        var commandBuffer = new EntityCommandBuffer(Allocator.Temp);


            Entities.ForEach((ref Entity entity, ref UnitComponentData unitComponent) =>
            {

                var targetFinder = EntityManager.GetComponentData<FindTargetData>(entity);
                if (targetFinder.targetGameObject != Entity.Null)
                {

                    // Get the translation components:
                    var target = targetFinder.targetGameObject;
                    var targetTranslation = EntityManager.GetComponentData<LocalTransform>(target);
                    var translation = EntityManager.GetComponentData<LocalTransform>(entity);
                    var dist = math.distance(targetTranslation.Position, translation.Position);

                    // Attack if target is in range and cooldown timer is finished:
                    if (dist < unitComponent.attackRange)
                    {
                        if (unitComponent.attackCoolDownTimer > 0)
                        {
                            unitComponent.attackCoolDownTimer -= 0.01f * unitComponent.attackSpeed;
                        }
                        else
                        {
                            var targetData = EntityManager.GetComponentData<UnitComponentData>(target);
                            targetData.hp -= unitComponent.attackDamage;

                            // Reset cool down timer:
                            unitComponent.attackCoolDownTimer = 1;
                            commandBuffer.SetComponent(targetFinder.targetGameObject, targetData);
                        }
                    }
                }

            }).WithStructuralChanges().Run();
            commandBuffer.Playback(EntityManager);
            commandBuffer.Dispose();
        }
    
}

