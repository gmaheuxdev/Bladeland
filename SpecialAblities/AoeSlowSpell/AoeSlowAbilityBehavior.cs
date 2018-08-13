using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeSlowAbilityBehavior : SpecialAbilityBehavior
{
    Collider[] m_ZoneOfEffectCollisions;
    Timer m_ActivationTimer;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (m_ActivationTimer.IsStarted())
        {
            m_ActivationTimer.m_TimeLeft -= Time.deltaTime;

            if (m_ActivationTimer.IsFinished())
            {
                m_ActivationTimer.StopTimer();
                ApplyAbilityEffect();
            }
        }
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void ApplyAbilityEffect()
    {
        m_ZoneOfEffectCollisions = Physics.OverlapSphere(m_AbilityCurrentTarget.transform.position, 2f);
        foreach (Collider currentCollider in m_ZoneOfEffectCollisions)
        {
            DamageComponent currentDamageComponent = currentCollider.gameObject.GetComponent<DamageComponent>();
            CharacterMovementComponent currentMovementComponent = currentCollider.gameObject.GetComponent<CharacterMovementComponent>();

            if (currentDamageComponent != null && currentMovementComponent != null)
            {
                if (m_AbilityOwnerDamageComponent.GetCurrentTeam() != currentDamageComponent.GetCurrentTeam())
                {
                    AoeSlowAbilityConfig abilityConfig = m_AbilityConfig as AoeSlowAbilityConfig;
                    currentMovementComponent.ApplyMovementSpeedModifier(abilityConfig.GetAbilitySlowPercentage(), abilityConfig.GetAbilityDuration());
                }
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnAoeSlowAnimationFinished()
    {
       if(m_AbilityCurrentTarget != null)
       {
            Instantiate((m_AbilityConfig as AoeSlowAbilityConfig).GetAbilityZoneOfEffectPrefab(), m_AbilityCurrentTarget.transform.position, Quaternion.identity);
            m_ActivationTimer.StartTimer(5f);
        }
    }
}
