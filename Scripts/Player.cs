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
    PlayerStatsComponent m_CachedPlayerStatsComponent;
    SpecialAbiltyComponent m_CachedPlayerSpecialAbilityComponent;
    
    //Sounds
    AudioClip[] m_DamageSounds;
    AudioClip[] m_DeathSounds;
    
    //Getters and setters
    private GameObject GetMainHand(){return gameObject.GetComponentInChildren<MainHand>().gameObject;}
    public Weapon GetEquippedWeapon(){return m_EquippedWeapon;}
        
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CachedCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_CachedPlayerAnimator = GetComponent<Animator>();
        m_CachedPlayerStatsComponent = GetComponent<PlayerStatsComponent>();
        m_CachedPlayerSpecialAbilityComponent = GetComponent<SpecialAbiltyComponent>();
        
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
            ManageLeftMouseClick();
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            ManageRightMouseClick();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("DoAbility1");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            print("DoAbility2");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("DoAbility3");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            print("DoAbility4");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            print("DoAbility5");
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            print("DoAbility6");
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            print("DoAbility7");
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            print("DoAbility8");
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            print("DoAbility9");
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ManageRightMouseClick()
    {
        if ((int)m_CachedCameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
            GameObject currentEnemyTarget = m_CachedCameraRaycaster.GetCurrentActiveHit().collider.gameObject;

            if (Vector3.Distance(transform.position, currentEnemyTarget.transform.position) < m_EquippedWeapon.GetWeaponAttackRange())
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

            if (Vector3.Distance(transform.position, currentEnemyTarget.transform.position) < m_EquippedWeapon.GetWeaponAttackRange())
            {
                m_CachedPlayerAnimator.SetTrigger("IsAttacking");
                DamageComponent currentEnemyDamageComponent = currentEnemyTarget.GetComponent<DamageComponent>();

                //Check for critical hit
                float critRoll = UnityEngine.Random.Range(0f, 1f);
                float damageToApply = m_playerDamage;

                if(critRoll < m_CachedPlayerStatsComponent.GetCritChance())
                {
                    damageToApply *= m_CachedPlayerStatsComponent.GetCritMultiplier();
                }
                
               currentEnemyDamageComponent.TakeDamage(damageToApply);
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
    
    public void PlayDamageSound()
    {
        print("DAMAGE SOUND");
    }

    public void PlayDeathSound()
    {
        print("DEATH SOUND");
    }

    public void PlayDeathAnimation()
    {
        m_CachedPlayerAnimator.SetTrigger("IsDead");
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,m_EquippedWeapon.GetWeaponAttackRange() );
    }

}//class end
