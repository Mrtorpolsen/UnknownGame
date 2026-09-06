using UnityEngine;

public class IntervalAbilityTrigger : AbilityTrigger
{
    private readonly float interval;
    private float nextTriggerTime;

    public IntervalAbilityTrigger(
        BaseUnitStats owner,
        float interval)
        : base(owner)
    {
        this.interval = interval;
        nextTriggerTime = Time.time + interval;
    }

    public override void Tick()
    {
        if (Time.time < nextTriggerTime)
            return;

        nextTriggerTime = Time.time + interval;

        Trigger();
    }
}