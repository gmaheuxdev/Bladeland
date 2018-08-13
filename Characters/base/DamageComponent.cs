using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DamageComponent : MonoBehaviour
{
    //Cached references
    CharacterStatsComponent m_CachedStatsComponent;
    AudioSource m_CachedAudioSource;
    Animator m_CachedAnimatorComponent;
    GameObject m_CachedPlayerGameObject; //Only on enemy //CHANGE THAT NAME
    CharacterTeamEnum m_CurrentTeam;

    //Serialized fields
    [SerializeField] private Image m_HealthBar;
    [SerializeField] private Image m_ManaBar;
    [SerializeField] private AudioClip m_DeathSound;
    [SerializeField] private AudioClip m_HurtSound;

    //Events
    public delegate void XPGainDelegate(int XPAmount);
    public event XPGainDelegate XpGainObservers;

    //Getters and Setters
    public CharacterTeamEnum GetCurrentTeam() { return m_CurrentTeam;}
    public void SetCurrentTeam(CharacterTeamEnum newTeamEnum) { m_CurrentTeam = newTeamEnum;}
    
    //////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_CachedStatsComponent = gameObject.GetComponent<CharacterStatsComponent>();
        m_CachedAudioSource = gameObject.GetComponent<AudioSource>();
        m_CachedAnimatorComponent = gameObject.GetComponent<Animator>();

        if(gameObject.layer == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
            m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player");//Replace with static helper?
            CharacterStatsComponent playerStatsComponent = m_CachedPlayerGameObject.GetComponent<CharacterStatsComponent>();
            XpGainObservers += playerStatsComponent.OnXpGainCallback;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        UpdateUI();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateUI()
    {
        if (m_HealthBar && m_ManaBar)
        {
            m_HealthBar.fillAmount = m_CachedStatsComponent.GetCurrentHealthPercentage();
            m_ManaBar.fillAmount = m_CachedStatsComponent.GetCurrentManaPercentage();
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(float damageToApply)
    {
        m_CachedStatsComponent.RemoveHealth(damageToApply);

        if (m_CachedStatsComponent.GetCurrentHealth() <= 0f)
        {
            Die();
        }
        else
        {
           PlayDamageSound();
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeHealing(float healAmount)
    {
        m_CachedStatsComponent.AddHealth(healAmount);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void Die()
    {
        PlayDeathSound();
        PlayDeathAnimation();
        DisableComponents();
               
        XpGainObservers(m_CachedStatsComponent.GetXPGivenOnDeath());
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void DisableComponents()
    {
        if (gameObject.layer == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Player)
        {
            GetComponent<PlayerController>().enabled = false;
        }
        else
        {
            GetComponent<EnemyAIController>().enabled = false;
        }

        GetComponent<CharacterMovementComponent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayDamageSound()
    {
        m_CachedAudioSource.PlayOneShot(m_HurtSound);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayDeathAnimation()
    {
        m_CachedAnimatorComponent.SetTrigger("IsDead");
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayDeathSound()
    {
        m_CachedAudioSource.PlayOneShot(m_DeathSound);
    }
}
