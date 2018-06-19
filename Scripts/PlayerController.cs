using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Member variables
    CameraRayCaster m_CachedCameraRaycaster;
    [SerializeField] private float  m_playerDamage;
    [SerializeField] private AnimatorOverrideController m_CachedPlayerAnimatorOverrideController;
    private Animator m_CachedPlayerAnimator;
    CharacterStatsComponent m_CachedPlayerStatsComponent;
    SpecialAbiltyComponent m_CachedPlayerSpecialAbilityComponent;
    CharacterMovementComponent m_CachedPlayerMovementComponent;
    WeaponComponent m_CachedPlayerWeaponComponent;

    //Sounds
    AudioClip[] m_DamageSounds;
    AudioClip[] m_DeathSounds;
        
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CachedCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_CachedPlayerAnimator = GetComponent<Animator>();
        m_CachedPlayerStatsComponent = GetComponent<CharacterStatsComponent>();
        m_CachedPlayerSpecialAbilityComponent = GetComponent<SpecialAbiltyComponent>();
        m_CachedPlayerMovementComponent = GetComponent<CharacterMovementComponent>();
        m_CachedPlayerWeaponComponent = GetComponent<WeaponComponent>();
        
        OverrideAnimatorController();
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
           TryDoRightClickAbility();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){print("DoAbility1");}
        if (Input.GetKeyDown(KeyCode.Alpha2)){print("DoAbility2");}
        if (Input.GetKeyDown(KeyCode.Alpha3)){print("DoAbility3");}
        if (Input.GetKeyDown(KeyCode.Alpha4)){print("DoAbility4");}
        if (Input.GetKeyDown(KeyCode.Alpha5)){print("DoAbility5");}
        if (Input.GetKeyDown(KeyCode.Alpha6)){print("DoAbility6");}
        if (Input.GetKeyDown(KeyCode.Alpha7)){print("DoAbility7");}
        if (Input.GetKeyDown(KeyCode.Alpha8)){print("DoAbility8");}
        if (Input.GetKeyDown(KeyCode.Alpha9)){print("DoAbility9");}
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void TryDoRightClickAbility()
    {
        //Can only be done on enemies in range
        if ((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
            GameObject currentEnemyTarget = m_CachedCameraRaycaster.GetCurrentActiveHit().collider.gameObject;

            if (TargetInAbilityRange(currentEnemyTarget))
            {
                m_CachedPlayerAnimator.SetTrigger("IsAttacking");
                m_CachedPlayerSpecialAbilityComponent.TryUseSpecialAbility(currentEnemyTarget);
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ManageLeftMouseClick()
    {
        if ((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
            GameObject currentEnemyTarget = m_CachedCameraRaycaster.GetCurrentActiveHit().collider.gameObject;

            if (TargetInWeaponRange(currentEnemyTarget))
            {
                m_CachedPlayerWeaponComponent.WeaponAttack(currentEnemyTarget);
            }
        }
        else if((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Walkable)
        {
            Vector3 TargetPosition = m_CachedCameraRaycaster.GetCurrentActiveHit().point;
            m_CachedPlayerMovementComponent.BeginMoveTo(TargetPosition);
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
            m_CachedPlayerMovementComponent.BeginMoveTo(TargetPosition);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool TargetInWeaponRange(GameObject target)
    {
        return Vector3.Distance(transform.position, target.transform.position) <
                                m_CachedPlayerWeaponComponent.GetEquippedWeaponConfig().GetWeaponAttackRange();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool TargetInAbilityRange(GameObject target)
    {
        return false;                               
    }
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OverrideAnimatorController()
    {
        m_CachedPlayerAnimator.runtimeAnimatorController = m_CachedPlayerAnimatorOverrideController;
        m_CachedPlayerAnimatorOverrideController["DEFAULTATTACKANIMATION"] = m_CachedPlayerWeaponComponent.GetEquippedWeaponConfig().GetWeaponAttackAnimation();
        m_CachedPlayerWeaponComponent.GetEquippedWeaponConfig().ClearAnimationEvents(); //Asset packs configuration can cause bugs without it
    }

      /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}//class end
