using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastAttackAbilityBehavior : SpecialAbilityBehavior
{
    public override void ApplyAbilityEffect()
    {
        CharacterMovementComponent ownerMovementComponent = m_AbilityOwner.GetComponent<CharacterMovementComponent>();

        if(ownerMovementComponent != null)
        {
            ownerMovementComponent.ApplyAttackSpeedModifier((m_AbilityConfig as FastAttackAbilityConfig).GetTimeActive());
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnFastAttackAnimationFinished()
    {
        m_AbilityOwnerAnimator.SetBool("IsDoSpecialAbility", false);
        m_AbilityOwnerAnimator.runtimeAnimatorController = m_AbilityOwnerWeaponComponent.GetActiveWeaponConfig().GetWeaponAnimatorOverride();
        ApplyAbilityEffect();
    }
}
