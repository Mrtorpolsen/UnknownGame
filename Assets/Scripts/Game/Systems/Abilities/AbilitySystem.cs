using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public static AbilitySystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool TryExecute(
        AbilityDefinition ability,
        AbilityContext context)
    {
        if (!AbilityCooldownManager.Instance.CanUse(
                ability,
                context.Caster))
        {
            return false;
        }

        foreach (var action in ability.actions)
        {
            action.Execute(context);
        }

        AbilityCooldownManager.Instance.TriggerCooldown(
            ability,
            context.Caster);

        return true;
    }
}
