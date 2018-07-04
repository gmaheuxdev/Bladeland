using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAttackBehavior : SpecialAbilityBehavior
{
    public override void Use()
    {
        PlayAbilityEffects();
        PlayAbilitySounds();
        PlayAbilityAnimation();
        ApplyPowerAttackDamage();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ApplyPowerAttackDamage()
    {
       // float totalDamage = (m_AbilityConfig as PowerAttackConfig).GetPowerAttackDamage();
       // DamageComponent targetDamageComponent = target.GetComponent<DamageComponent>();
    }
}
