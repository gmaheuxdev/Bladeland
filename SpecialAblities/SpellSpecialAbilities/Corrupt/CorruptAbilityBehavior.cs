using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptAbilityBehavior : SpecialAbilityBehavior
{
    bool isActive;
    int  ticksLeft;
    DamageComponent targetDamageComponent;
    Timer waitForTickTimer;

    void Update()
    {
        if(isActive)
        {
            if (waitForTickTimer.IsStarted())
            {
                waitForTickTimer.m_TimeLeft -= Time.deltaTime;

                if (waitForTickTimer.IsFinished())
                {
                    ApplyTickDamageToTarget();
                    ticksLeft--;
                    if (ticksLeft <= 0)
                    {
                        waitForTickTimer.StopTimer();
                    }
                    else
                    {
                        waitForTickTimer.StartTimer((m_AbilityConfig as CorruptAbilityConfig).GetTimeBetweenTicks());
                    }
                }
            }
        }
    }

    void Start()
    {
        base.Start();
        isActive = false;
        ticksLeft = (m_AbilityConfig as CorruptAbilityConfig).GetTickAmount();
    }

    public override void ApplyAbilityEffect()
    {
        isActive = true;
        waitForTickTimer.StartTimer((m_AbilityConfig as CorruptAbilityConfig).GetTimeBetweenTicks());
    }

    void OnCorruptAnimationFinished()
    {
        m_AbilityOwnerAnimator.SetBool("IsDoSpecialAbility", false);
        m_AbilityOwnerAnimator.runtimeAnimatorController = m_AbilityOwnerWeaponComponent.GetActiveWeaponConfig().GetWeaponAnimatorOverride();
        ApplyAbilityEffect();
    }

    void ApplyTickDamageToTarget()
    {
        targetDamageComponent = m_AbilityCurrentTarget.GetComponent<DamageComponent>();

        if(targetDamageComponent != null)
        {
            targetDamageComponent.TakeDamage(10);
        }
    }
}
