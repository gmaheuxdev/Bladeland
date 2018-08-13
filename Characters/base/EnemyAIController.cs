using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AIState {AIState_Idle,AIState_Chase,AIState_Attack,AIState_Patrol};

public abstract class EnemyAIController : MonoBehaviour
{
    //Serialized variables
    [SerializeField] protected float m_AggroRange;
    [SerializeField] protected float m_ChaseRange;
    [SerializeField] protected float m_AttackRange; //REPLACE WITH WEAPON WHEN WEAPONED AND ABILITY WHEN ABILITIED


    //Cached components
    protected GameObject m_CachedPlayerGameObject;
    protected CharacterMovementComponent m_CachedMovementComponent;
    protected WeaponComponent m_CachedWeaponComponent;
    protected Animator m_CachedAnimatorComponent;
    protected DamageComponent m_CachedDamageComponent;
    protected SpecialAbiltyComponent m_CachedSpecialAbilityComponent;

    //Member variables
    float m_AttackTimer;
    protected AIState m_CurrentAIState;

    //Abstract methods
    protected abstract void DoPatrolBehavior();
    protected abstract void DoAttackBehavior();
    protected abstract void DoChaseBehavior();
    protected abstract void DoIdleBehavior();

    //Getters and setters
    public AIState GetCurrentAIState() {return m_CurrentAIState;}

///////////////////////////////////////////////////////////////////////////////////////////////////////
   void Start()
   {
        m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player");//Replace with static helper?
        m_CachedMovementComponent = GetComponent<CharacterMovementComponent>();
        m_CachedWeaponComponent = GetComponent<WeaponComponent>();
        m_CachedAnimatorComponent = GetComponent<Animator>();
        m_CachedDamageComponent = GetComponent<DamageComponent>();
        m_CachedSpecialAbilityComponent = GetComponent<SpecialAbiltyComponent>();

        m_CachedWeaponComponent.SetCurrentTarget(m_CachedPlayerGameObject);//Target will always be player
        m_CurrentAIState = AIState.AIState_Idle;
        m_CachedDamageComponent.SetCurrentTeam(CharacterTeamEnum.CharacterTeamEnum_AI);
    }

///////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(m_CurrentAIState == AIState.AIState_Idle)
        {
            DoIdleBehavior();
        }

        if(m_CurrentAIState == AIState.AIState_Attack)
        {
            DoAttackBehavior();
        }

        if(m_CurrentAIState == AIState.AIState_Chase)
        {
            DoChaseBehavior();
        }

        if(m_CurrentAIState == AIState.AIState_Patrol)
        {
            DoPatrolBehavior();
        }
   }

   ///////////////////////////////////////////////////////////////////////////////////////////////////////
   protected bool IsPlayerInAggroRange()
   {
        if(Vector3.Distance(transform.position,m_CachedPlayerGameObject.transform.position) < m_AggroRange)
        {
            return true;
        }

        return false;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool IsPlayerInChaseRange()
    {
        return Vector3.Distance(transform.position, m_CachedPlayerGameObject.transform.position) < m_ChaseRange;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    protected bool IsTargetInAttackRange()
    {
        return Vector3.Distance(transform.position, m_CachedPlayerGameObject.transform.position) < m_AttackRange;
    }
}
