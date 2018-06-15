using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAttackBehavior : SpecialAbilityBehavior
{
    public override void Use(GameObject target)
    {
        PlayAbilityEffects();
        PlayAbilitySounds();
        PlayAbilityAnimation();
        ApplyPowerAttackDamage(target);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ApplyPowerAttackDamage(GameObject target)
    {
        float totalDamage = (m_AbilityConfig as PowerAttackConfig).GetPowerAttackDamage();
        DamageComponent targetDamageComponent = target.GetComponent<DamageComponent>();
    }
}
