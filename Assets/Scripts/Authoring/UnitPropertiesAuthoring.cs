using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class UnitPropertiesAuthoring : MonoBehaviour
{
    public float hp;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float movementSpeed;
    public bool isTeamBlue;
    public float attackCoolDownTimer;
    public int unitNum =0;


    public class Baker : Baker<UnitPropertiesAuthoring>
    {
        public override void Bake(UnitPropertiesAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new UnitComponentData
            {
                hp = authoring.hp,
                attackDamage = authoring.attackDamage,
                attackSpeed = authoring.attackSpeed,
                attackRange = authoring.attackRange,
                movementSpeed = authoring.movementSpeed,
                isTeamBlue = authoring.isTeamBlue,
                attackCoolDownTimer = authoring.attackCoolDownTimer,
                unitNum = authoring.unitNum,
            });
        }
    }
}


public struct UnitComponentData : IComponentData
{
    public float hp;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float movementSpeed;
    public bool isTeamBlue;
    public float attackCoolDownTimer;
    public int unitNum;

}
