using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Member variables
    CameraRayCaster m_CachedCameraRaycaster;
    CharacterMovementComponent m_CachedMovementComponent;
    WeaponComponent m_CachedWeaponComponent;
    DamageComponent m_CachedDamageComponent;
    SpecialAbilityConfig m_ActiveAbility;
       
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CachedCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_CachedMovementComponent = GetComponent<CharacterMovementComponent>();
        m_CachedWeaponComponent = GetComponent<WeaponComponent>();
        m_CachedDamageComponent = GetComponent<DamageComponent>();
        m_CachedDamageComponent.SetCurrentTeam(CharacterTeamEnum.CharacterTeamEnum_Player);
    }
         
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update ()
    {
        CheckForAbilityInput();

        if (Input.GetMouseButton(0)) //Mouse left
        {
            ManageMouseMovement();
        }

        if(Input.GetMouseButtonUp(0))
        {
            ManageLeftMouseClick();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_CachedWeaponComponent.CycleActiveWeapon();
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void CheckForAbilityInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(m_ActiveAbility != null)
            {
                m_ActiveAbility.UseSpecialAbility();
            }
        }
                
        if (Input.GetKeyDown(KeyCode.Alpha1)){ m_ActiveAbility = m_CachedWeaponComponent.GetWeaponSpecialAbilities()[0];}
        if (Input.GetKeyDown(KeyCode.Alpha2)){ m_ActiveAbility = m_CachedWeaponComponent.GetWeaponSpecialAbilities()[1];}
        if (Input.GetKeyDown(KeyCode.Alpha3)){ m_ActiveAbility = m_CachedWeaponComponent.GetWeaponSpecialAbilities()[2];}
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
               m_CachedWeaponComponent.WeaponAttack();
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
