using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldownManager : MonoBehaviour
{
    public static AbilityCooldownManager Instance { get; private set; }

    private Dictionary<string, float> lastUseTime = new();

    public event Action<string> OnCooldownTriggered;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool CanUse(AbilityDefinition ability, BaseUnitStats caster = null)
    {
        string key = GetCooldownKey(ability, caster);

        if (!lastUseTime.TryGetValue(key, out float last))
            return true;

        return Time.time >= last + ability.cooldown;
    }

    public float GetRemainingCooldown(AbilityDefinition ability, BaseUnitStats caster = null)
    {
        string key = GetCooldownKey(ability, caster);

        if (!lastUseTime.TryGetValue(key, out float last))
            return 0;

        return Mathf.Max(0, last + ability.cooldown - Time.time);
    }

    public float GetRemainingCooldown(string coolDownKey, float cooldown)
    {
        if (!lastUseTime.TryGetValue(coolDownKey, out float last))
            return 0;

        return Mathf.Max(0, last + cooldown - Time.time);
    }

    public void TriggerCooldown(AbilityDefinition ability, BaseUnitStats caster = null)
    {
        string key = GetCooldownKey(ability, caster);

        lastUseTime[key] = Time.time;
        OnCooldownTriggered?.Invoke(key);
    }

    public string GetCooldownKey(AbilityDefinition ability, BaseUnitStats caster = null)
    {
        if (caster == null)
            return ability.Id;

        return $"{ability.Id}_{caster.GetInstanceID()}";
    }
}