using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Member variables
    CameraRayCaster m_CachedCameraRaycaster;
    [SerializeField] private float  m_playerDamage;
    [SerializeField] private Weapon m_EquippedWeapon;
    [SerializeField] private GameObject m_EquippedWeaponSocket;
    [SerializeField] private AnimatorOverrideController m_CachedPlayerAnimatorOverrideController;
    private Animator m_CachedPlayerAnimator;

    //Getters and setters
    private GameObject GetMainHand(){return gameObject.GetComponentInChildren<MainHand>().gameObject;}
        
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CachedCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_CachedPlayerAnimator = GetComponent<Animator>();
        EquipWeapon();
        OverrideAnimatorController();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OverrideAnimatorController()
    {
        m_CachedPlayerAnimator.runtimeAnimatorController = m_CachedPlayerAnimatorOverrideController;
        m_CachedPlayerAnimatorOverrideController["DEFAULTATTACKANIMATION"] = m_EquippedWeapon.GetWeaponAttackAnimation();
        m_EquippedWeapon.ClearAnimationEvents(); //Asset packs configuration can cause bugs without it
    }
    
 
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update ()
    {
        if(Input.GetMouseButton(0))
        {
            ManageMouseClick();
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ManageMouseClick()
    {
        if ((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
            GameObject currentEnemyTarget = m_CachedCameraRaycaster.GetCurrentActiveHit().collider.gameObject;

            if (Vector3.Distance(transform.position, currentEnemyTarget.transform.position) < m_EquippedWeapon.GetWeaponAttackRange())
            {
                m_CachedPlayerAnimator.SetTrigger("IsAttacking");
                EnemyDamageComponent currentEnemyDamageComponent = currentEnemyTarget.GetComponent<EnemyDamageComponent>();
                currentEnemyDamageComponent.TakeDamage(m_playerDamage);
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void EquipWeapon()
    {
        m_EquippedWeaponSocket = GetMainHand();
        GameObject weaponToEquipPrefab = m_EquippedWeapon.GetWeaponPrefab();
        GameObject weaponToEquipInstance = Instantiate(weaponToEquipPrefab,m_EquippedWeaponSocket.transform);
        weaponToEquipInstance.transform.localPosition = m_EquippedWeapon.GetEquippedWeaponPosRotPreset().transform.localPosition;
        weaponToEquipInstance.transform.localRotation = m_EquippedWeapon.GetEquippedWeaponPosRotPreset().transform.localRotation;
    }
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,m_EquippedWeapon.GetWeaponAttackRange() );
    }

}//class end
