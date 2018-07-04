using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHealAbilityBehavior : SpecialAbilityBehavior
{
    DamageComponent m_CachedDamageComponent;
    
    private void Start()
    {
        m_CachedDamageComponent = GetComponent<DamageComponent>();
    }
    
    public override void Use()
    {
        PlayAbilitySounds();
        ApplyHealing();
    }

    void ApplyHealing()
    {
        float totalHealing = (m_AbilityConfig as AutoHealAbilityConfig).GetHealingAmount();
        m_CachedDamageComponent.TakeHealing(totalHealing);
    }
}
