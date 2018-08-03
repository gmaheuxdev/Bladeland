using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedAbilityBehavior : SpecialAbilityBehavior
{
    bool m_ShouldApplyEffect;
    int m_TicksLeft;
    float m_TimeBetweenTicks;
    float m_TimeLeftBeforeTick;
            
    new void Start()
    {
        base.Start();
        m_ShouldApplyEffect = false;
    }

    void Update()
    {
        if (m_ShouldApplyEffect)
        {
            if(m_TicksLeft > 0)
            {
                if (m_TimeLeftBeforeTick <= 0)
                {
                    DoTickDamage();
                    m_TimeLeftBeforeTick = m_TimeBetweenTicks;
                    m_TicksLeft--;
                }
                else
                {
                    m_TimeLeftBeforeTick -= Time.deltaTime;
                }
            }
            else
            {
                m_ShouldApplyEffect = false;
            }
        }
    }

    public override void ApplyAbilityEffect()
    {
        if (Vector3.Distance(m_AbilityCurrentTarget.transform.position, m_AbilityOwner.transform.position) < m_AbilityConfig.GetAbilityMaxRange())
        {
            m_ShouldApplyEffect = true;
            m_TicksLeft = (GetAbilityConfig() as BleedAbilityConfig).GetTickAmount();
            m_TimeBetweenTicks = (GetAbilityConfig() as BleedAbilityConfig).GetTimeBetweenTicks();
            m_TimeLeftBeforeTick = m_TimeBetweenTicks;
        }
    }

    private void DoTickDamage()
    {
        print("DO TICK DAMAGE");
    }
}
