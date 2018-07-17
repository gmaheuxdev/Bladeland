using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAttackBehavior : SpecialAbilityBehavior
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void Use()
    {
        SetAbilityAnimationOverride(m_AbilityConfig.GetAbilityAnimationOverride());
        m_AbilityOwner.GetComponent<Animator>().SetBool("IsDoSpecialAbility", true);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnPowerAttackAnimationFinished()
    {
        m_AbilityOwner.GetComponent<Animator>().SetBool("IsDoSpecialAbility", false);
        m_AbilityOwner.GetComponent<LightningBlastBehavior>().ApplyAbilityEffect();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void ApplyAbilityEffect()
    {
        GameObject abilityTarget = m_AbilityOwner.GetComponent<WeaponComponent>().GetCurrentTarget();
        float totalDamage = (m_AbilityConfig as PowerAttackConfig).GetPowerAttackDamage();
        DamageComponent targetDamageComponent = abilityTarget.GetComponent<DamageComponent>();
        targetDamageComponent.TakeDamage(totalDamage);
        PlayAbilitySounds();
    }
}
