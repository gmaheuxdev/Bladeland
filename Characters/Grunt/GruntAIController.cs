using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAIController : EnemyAIController
{
    //member variables
    Timer m_AttackTimer;
    [SerializeField]
    float m_TimeBetweenAttacks;

    ///////////////////////////////////////////////////////////////////////////////////
    protected override void DoIdleBehavior()
    {
        if (ShouldAttack())
        {
            m_CurrentAIState = AIState.AIState_Attack;
        }
        else if (ShouldAggro()) //because aggro range < chase range and can only aggro when idle
        {
            m_CurrentAIState = AIState.AIState_Chase;
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////
    protected override void DoChaseBehavior()
    {
        if (ShouldIdle())
        {
            m_CurrentAIState = AIState.AIState_Idle;
        }
        else if (ShouldAttack())
        {
            m_CurrentAIState = AIState.AIState_Attack;
        }
        else
        {
            m_CachedMovementComponent.BeginMoveTo(m_CachedPlayerGameObject.transform.position);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////
    protected override void DoAttackBehavior()
    {
        if (ShouldIdle())
        {
            m_CurrentAIState = AIState.AIState_Idle;
        }
        else if (ShouldChase())
        {
            m_CurrentAIState = AIState.AIState_Chase;
        }
        else
        {
            BeginAttack();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////
    protected override void DoPatrolBehavior()
    {

    }

    ///////////////////////////////////////////////////////////////////////////////////
    private bool ShouldChase()
    {
        return IsPlayerInChaseRange() && !IsTargetInAttackRange();
    }

    ///////////////////////////////////////////////////////////////////////////////////
    private bool ShouldAttack()
    {
        return IsTargetInAttackRange();
    }

    ///////////////////////////////////////////////////////////////////////////////////
    private bool ShouldIdle()
    {
        if (!IsPlayerInChaseRange()) { return true; }
        else return false;
    }

    ///////////////////////////////////////////////////////////////////////////////////
    private bool ShouldAggro()
    {
        return IsPlayerInAggroRange();
    }

    ///////////////////////////////////////////////////////////////////////////////////
    private void BeginAttack()
    {
        if (!m_AttackTimer.IsStarted())
        {
            m_AttackTimer.StartTimer(m_TimeBetweenAttacks);
        }

        else if (m_AttackTimer.IsFinished())
        {
            int attackTypeRoll = UnityEngine.Random.Range(1, 100);

            if (attackTypeRoll < 80)
            {
                m_CachedWeaponComponent.WeaponAttack();
            }
            else if(attackTypeRoll > 80 && attackTypeRoll <90)
            {
                GetComponent<SpecialAbiltyComponent>().UseSpecialAbility(0);
            }
            else
            {
                GetComponent<SpecialAbiltyComponent>().UseSpecialAbility(1);
            }

            m_AttackTimer.StopTimer();
            m_AttackTimer.StartTimer(m_TimeBetweenAttacks);
        }
        else
        {
            m_AttackTimer.m_TimeLeft -= Time.deltaTime;
        }
    }
}
