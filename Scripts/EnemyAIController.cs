using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    GameObject m_CachedPlayerGameObject;
    CharacterMovementComponent m_CachedMovementComponent;
    WeaponComponent m_CachedWeaponComponent;

///////////////////////////////////////////////////////////////////////////////////////////////////////
   void Start()
   {
        m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player");//Replace with static helper?
        m_CachedMovementComponent = GetComponent<CharacterMovementComponent>();
        m_CachedWeaponComponent = GetComponent<WeaponComponent>();
   }

///////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(TargetInWeaponRange())
        {
            m_CachedWeaponComponent.WeaponAttack(m_CachedPlayerGameObject);
        }
        else
        {
            m_CachedMovementComponent.BeginMoveTo(m_CachedPlayerGameObject.transform.position);
        }
   }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool TargetInWeaponRange()
    {
        return Vector3.Distance(transform.position, m_CachedPlayerGameObject.transform.position) <
                                m_CachedWeaponComponent.GetEquippedWeaponConfig().GetWeaponAttackRange();
    }
}
