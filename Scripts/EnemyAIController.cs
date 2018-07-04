using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    GameObject m_CachedPlayerGameObject;
    CharacterMovementComponent m_CachedMovementComponent;
    WeaponComponent m_CachedWeaponComponent;
    float m_AttackTimer;

///////////////////////////////////////////////////////////////////////////////////////////////////////
   void Start()
   {
        m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player");//Replace with static helper?
        m_CachedMovementComponent = GetComponent<CharacterMovementComponent>();
        m_CachedWeaponComponent = GetComponent<WeaponComponent>();
        m_AttackTimer = m_CachedWeaponComponent.GetEquippedWeaponConfig().GetWeaponTimeBetweenAttacks();

        m_CachedWeaponComponent.SetCurrentTarget(m_CachedPlayerGameObject);//Target will always be player
    }

///////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (m_CachedWeaponComponent && m_AttackTimer <=0 && m_CachedWeaponComponent.IsTargetInWeaponRange())
        {
            m_CachedWeaponComponent.WeaponAttack();
            m_AttackTimer = m_CachedWeaponComponent.GetEquippedWeaponConfig().GetWeaponTimeBetweenAttacks();
        }
        else
        {
            m_AttackTimer -= Time.deltaTime;
            m_CachedMovementComponent.BeginMoveTo(m_CachedPlayerGameObject.transform.position);
        }
   }
}
