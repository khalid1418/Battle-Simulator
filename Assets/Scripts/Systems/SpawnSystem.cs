using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Unity.Burst;
using UnityEngine;


public partial struct SpawnSystem : ISystem
{
    private GameManagerComponentData gameManager;
    private EntityManager entityManager;
    

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnData>();
        entityManager = state.EntityManager;

    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
            var unitNumberAdd = 1;
            var entities = entityManager.GetAllEntities();

            foreach (var entity in entities)
            {
                if (entityManager.HasComponent<GameManagerComponentData>(entity))
                {
                    var battleMgr = entityManager.GetComponentData<GameManagerComponentData>(entity);
                    gameManager = battleMgr;
                }
            }
            state.Enabled = false;
            var spawnData = SystemAPI.GetSingleton<SpawnData>();
            var blueunitTransform = state.EntityManager.GetComponentData<LocalTransform>(spawnData.blueUnit);
            var redunitTransform = state.EntityManager.GetComponentData<LocalTransform>(spawnData.redUnit);
            var gridSize = 2;
            for (int i = 0; i < spawnData.unitCount; i++)
            {
                int row = i / gridSize;
                int col = i % gridSize;
                var blueSpawnEntity = state.EntityManager.Instantiate(spawnData.blueUnit);
                var redSpawnEntity = state.EntityManager.Instantiate(spawnData.redUnit);
                blueunitTransform.Position.x = row * 1.5f;
                blueunitTransform.Position.z = col * 1.5f;
                redunitTransform.Position.x = row * 1.5f;
                redunitTransform.Position.z = col * 1.5f + 10f;
                state.EntityManager.SetComponentData(blueSpawnEntity, blueunitTransform);
                var componentData = state.EntityManager.GetComponentData<UnitComponentData>(blueSpawnEntity);
                var componentDataRed = state.EntityManager.GetComponentData<UnitComponentData>(redSpawnEntity);

                componentData.hp = gameManager.HP;
                componentData.attackDamage = gameManager.Attack;
                componentData.attackRange = gameManager.AttackRange;
                componentData.attackSpeed = gameManager.AttackSpeed;
                componentData.movementSpeed = gameManager.MovementSpeed;
                componentData.unitNum = unitNumberAdd;
                unitNumberAdd++;


                state.EntityManager.SetComponentData(blueSpawnEntity, componentData);
                state.EntityManager.SetComponentData(redSpawnEntity, redunitTransform);


                componentDataRed.hp = gameManager.HPRed;
                componentDataRed.attackDamage = gameManager.AttackRed;
                componentDataRed.attackRange = gameManager.AttackRangeRed;
                componentDataRed.attackSpeed = gameManager.AttackSpeedRed;
                componentDataRed.movementSpeed = gameManager.MovementSpeedRed;
                componentDataRed.unitNum = unitNumberAdd;
                unitNumberAdd++;
                state.EntityManager.SetComponentData(redSpawnEntity, componentDataRed);

            }


        }
    }





