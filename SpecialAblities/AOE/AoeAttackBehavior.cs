using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttackBehavior : SpecialAbilityBehavior
{
    public override void Use()
    {
        RaycastHit[] sphereCastHitList = Physics.SphereCastAll(gameObject.transform.position, (m_AbilityConfig as AoeAttackConfig).GetAoeRadius(), Vector3.up);
        ApplyRadialDamage(sphereCastHitList);
        PlayAbilityEffects();
        SetAbilityAnimationOverride(m_AbilityConfig.GetAbilityAnimationOverride());
        PlayAbilitySounds();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    public override void ApplyAbilityEffect()
    {

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    private void ApplyRadialDamage(RaycastHit[] sphereCastHitList)
    {
        foreach (RaycastHit currentHit in sphereCastHitList)
        {
            DamageComponent currentEnemyDamageComponent = currentHit.collider.gameObject.GetComponent<DamageComponent>();

            if (currentEnemyDamageComponent != null)
            {
                float totalDamage = (m_AbilityConfig as AoeAttackConfig).GetAoeDamage();
                currentEnemyDamageComponent.TakeDamage(totalDamage);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, (m_AbilityConfig as AoeAttackConfig).GetAoeRadius());
    }
}
