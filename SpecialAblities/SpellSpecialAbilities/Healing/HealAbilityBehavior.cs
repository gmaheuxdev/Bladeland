using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealAbilityBehavior : SpecialAbilityBehavior
{
    public override void ApplyAbilityEffect()
    {
        DamageComponent ownerDamageComponent = gameObject.GetComponent<DamageComponent>();

        if (ownerDamageComponent != null)
        {
            ownerDamageComponent.TakeHealing((m_AbilityConfig as HealAbilityConfig).GetHealAmount());
        }
    }

    void OnHealAbilityAnimationFinished()
    {
        m_AbilityOwnerAnimator.SetBool("IsDoSpecialAbility", false);
        m_AbilityOwnerAnimator.runtimeAnimatorController = m_AbilityOwner.GetComponent<WeaponComponent>().GetActiveWeaponConfig().GetWeaponAnimatorOverride();
        ApplyAbilityEffect();
    }
}
