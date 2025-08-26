using UnityEngine;

public interface IUnit : ITargetable
{
    float GetAttackRange();
    int GetAttackDamage();
    float GetAttackSpeed();
    float GetMovementSpeed();

}