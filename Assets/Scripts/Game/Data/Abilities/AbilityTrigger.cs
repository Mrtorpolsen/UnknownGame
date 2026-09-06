using System;

public abstract class AbilityTrigger
{
    protected readonly BaseUnitStats owner;

    public event Action Triggered;

    protected AbilityTrigger(BaseUnitStats owner)
    {
        this.owner = owner;
    }

    protected void Trigger()
    {
        Triggered?.Invoke();
    }

    public virtual void Tick() { }

    public virtual void Dispose() { }
}