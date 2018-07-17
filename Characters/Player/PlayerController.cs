using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Member variables
    CameraRayCaster m_CachedCameraRaycaster;
    SpecialAbiltyComponent m_CachedSpecialAbilityComponent;
    CharacterMovementComponent m_CachedMovementComponent;
    WeaponComponent m_CachedWeaponComponent;
    DamageComponent m_CachedDamageComponent;
       
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CachedCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_CachedSpecialAbilityComponent = GetComponent<SpecialAbiltyComponent>();
        m_CachedMovementComponent = GetComponent<CharacterMovementComponent>();
        m_CachedWeaponComponent = GetComponent<WeaponComponent>();
        m_CachedDamageComponent = GetComponent<DamageComponent>();

        m_CachedDamageComponent.SetCurrentTeam(CharacterTeamEnum.CharacterTeamEnum_Player);
    }
         
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update ()
    {
        CheckForAbilityInput();

        if (Input.GetMouseButton(0))
        {
            ManageMouseMovement();
        }

        if(Input.GetMouseButtonUp(0))
        {
            ManageLeftMouseClick();
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void CheckForAbilityInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_CachedSpecialAbilityComponent.UseSpecialAbility(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(1); }
        if (Input.GetKeyDown(KeyCode.Alpha2)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(2); }
        if (Input.GetKeyDown(KeyCode.Alpha3)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(3); }
        if (Input.GetKeyDown(KeyCode.Alpha4)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(4); }
        if (Input.GetKeyDown(KeyCode.Alpha5)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(5); }
        if (Input.GetKeyDown(KeyCode.Alpha6)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(6); }
        if (Input.GetKeyDown(KeyCode.Alpha7)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(7); }
        if (Input.GetKeyDown(KeyCode.Alpha8)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(8); }
        if (Input.GetKeyDown(KeyCode.Alpha9)){ m_CachedSpecialAbilityComponent.UseSpecialAbility(9); }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ManageLeftMouseClick()
    {
        if ((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
            GameObject currentEnemyTarget = m_CachedCameraRaycaster.GetCurrentActiveHit().collider.gameObject;
            m_CachedWeaponComponent.SetCurrentTarget(currentEnemyTarget);

            if (m_CachedWeaponComponent.IsTargetInWeaponRange())
            {
               // m_CachedPlayerWeaponComponent.WeaponAttack();
            }
        }
        else if((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Walkable)
        {
            Vector3 TargetPosition = m_CachedCameraRaycaster.GetCurrentActiveHit().point;
            m_CachedMovementComponent.BeginMoveTo(TargetPosition);
        }
        else
        {
            ManageMouseMovement();
        }
    }

    private void ManageMouseMovement()
    {
        if ((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Walkable)
        {
            Vector3 TargetPosition = m_CachedCameraRaycaster.GetCurrentActiveHit().point;
            m_CachedMovementComponent.BeginMoveTo(TargetPosition);
        }
    }

}//class end
