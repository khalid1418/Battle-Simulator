
using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Config" , menuName ="Config Model")]
[Serializable]
public class ConfigModel : ScriptableObject
{
    public Config[] configs;

}
[Serializable]
public class Config
{
    public UnitProperties TeamAProperties;
    public UnitProperties TeamBProperties;
}
[Serializable]
public class UnitProperties
{
    public float HP;
    public float Attack;
    public float AttackSpeed;
    public float AttackRange;
    public float MovementSpeed;
}
