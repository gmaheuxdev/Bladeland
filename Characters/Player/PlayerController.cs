using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InputContextEnum { InputContextEnum_NormalMode, InputContextEnum_AimMode };

public class PlayerController : MonoBehaviour
{
    //Member variables
    CameraRayCaster m_CachedCameraRaycaster;
    CharacterMovementComponent m_CachedMovementComponent;
    WeaponComponent m_CachedWeaponComponent;
    DamageComponent m_CachedDamageComponent;
    SpecialAbilityConfig m_ActiveAbility;
    Vector3 m_ClickTargetPosition;
    InputContextEnum m_InputContext;


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_CachedCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_CachedMovementComponent = GetComponent<CharacterMovementComponent>();
        m_CachedWeaponComponent = GetComponent<WeaponComponent>();
        m_CachedDamageComponent = GetComponent<DamageComponent>();
        m_CachedDamageComponent.SetCurrentTeam(CharacterTeamEnum.CharacterTeamEnum_Player);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        CheckForAbilityInput();

        if (m_InputContext == InputContextEnum.InputContextEnum_NormalMode)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                DoNormalModeLeftClick();
            }
        }

        else if(m_InputContext == InputContextEnum.InputContextEnum_AimMode)
        {
           if(Input.GetMouseButtonDown(0))
            {
                DoAimModeLeftClick();
           }
        }
 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_CachedWeaponComponent.CycleActiveWeapon();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(m_InputContext != InputContextEnum.InputContextEnum_AimMode) { m_InputContext = InputContextEnum.InputContextEnum_AimMode;}
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_CachedMovementComponent.SetIsAimMode(false);
            m_InputContext = InputContextEnum.InputContextEnum_NormalMode;
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
    private void DoNormalModeLeftClick()
    {
        m_ClickTargetPosition = m_CachedCameraRaycaster.GetCurrentActiveHit().point;

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
            m_CachedMovementComponent.BeginMoveTo(m_ClickTargetPosition);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void DoAimModeLeftClick()
    {
        m_CachedMovementComponent.BeginRotateTo(m_CachedCameraRaycaster.GetCurrentActiveHit().point);
        m_CachedWeaponComponent.WeaponAttack();
    }

}//class end
