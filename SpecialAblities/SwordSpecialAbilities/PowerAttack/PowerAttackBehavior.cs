using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAttackBehavior : SpecialAbilityBehavior
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void ApplyAbilityEffect()
    {
        GameObject abilityTarget = m_AbilityOwnerWeaponComponent.GetCurrentTarget();

        if (abilityTarget != null)
        {
            float totalDamage = (m_AbilityConfig as PowerAttackConfig).GetPowerAttackDamage();

            DamageComponent targetDamageComponent = abilityTarget.GetComponent<DamageComponent>();
            targetDamageComponent.TakeDamage(totalDamage);
        }
        PlayAbilitySounds();
    }
}
