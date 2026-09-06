using UnityEngine;

[CreateAssetMenu(menuName = "Game/Ability Triggers/Interval")]
public class IntervalTriggerDefinition : AbilityTriggerDefinition
{
    [SerializeField] private float interval;

    public override AbilityTrigger Create(BaseUnitStats owner)
    {
        return new IntervalAbilityTrigger(owner, interval);
    }
}