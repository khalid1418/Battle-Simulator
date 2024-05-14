using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameManagerAuthoring : MonoBehaviour
{
    public ConfigModel configModel;
    public int selectedIndex;
    public bool battleStart;


    public class Baker : Baker<GameManagerAuthoring>
    {

        public override void Bake(GameManagerAuthoring authoring)
        {
            Debug.Log("itStart");
            var entity = GetEntity(TransformUsageFlags.None);
            int selectIndex = authoring.selectedIndex;
            AddComponent(entity, new GameManagerComponentData
            {
                
                battleStart = authoring.battleStart,
                HP = authoring.configModel.configs[selectIndex].TeamAProperties.HP,
                Attack = authoring.configModel.configs[selectIndex].TeamAProperties.Attack,
                AttackSpeed = authoring.configModel.configs[selectIndex].TeamAProperties.AttackSpeed,
                AttackRange = authoring.configModel.configs[selectIndex].TeamAProperties.AttackRange,
                MovementSpeed = authoring.configModel.configs[selectIndex].TeamAProperties.MovementSpeed,
                HPRed = authoring.configModel.configs[selectIndex].TeamBProperties.HP,
                AttackRed = authoring.configModel.configs[selectIndex].TeamBProperties.Attack,
                AttackRangeRed = authoring.configModel.configs[selectIndex].TeamBProperties.AttackRange,
                AttackSpeedRed = authoring.configModel.configs[selectIndex].TeamBProperties.AttackSpeed,
                MovementSpeedRed = authoring.configModel.configs[selectIndex].TeamBProperties.MovementSpeed,


            });
        }
    }


}

public struct GameManagerComponentData : IComponentData
{
    public float HP;
    public float Attack;
    public float AttackSpeed;
    public float AttackRange;
    public float MovementSpeed;
    public bool battleStart;
    // Team Red
    public float HPRed;
    public float AttackRed;
    public float AttackSpeedRed;
    public float AttackRangeRed;
    public float MovementSpeedRed;
}
