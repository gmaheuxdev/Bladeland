using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStunAbilityBehavior : SpecialAbilityBehavior
{

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void ApplyAbilityEffect()
    {
        ActivateStunEffect();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ActivateStunEffect()
    {
        float stunDuration = (m_AbilityConfig as MeleeStunAbilityConfig).GetAbilityEffectDuration();
        GameObject target = m_AbilityOwner.GetComponent<WeaponComponent>().GetCurrentTarget();
        target.GetComponent<CharacterMovementComponent>().ApplyMovementSpeedModifier(0.01f, stunDuration);
    }
}
