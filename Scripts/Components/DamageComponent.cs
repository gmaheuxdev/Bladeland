using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageComponent : MonoBehaviour
{
    //Member variables
    PlayerStatsComponent m_CachedPlayerStatsComponent;
    Player m_CachedPlayerComponent;
    [SerializeField] private Image m_HealthOrbImage;
    [SerializeField] private Image m_ManaOrbImage;
    
    //////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_CachedPlayerStatsComponent = gameObject.GetComponent<PlayerStatsComponent>();
        m_CachedPlayerComponent = GetComponent<Player>(); 
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        m_HealthOrbImage.fillAmount = m_CachedPlayerStatsComponent.GetPlayerCurrentHealthPercentage();
        m_ManaOrbImage.fillAmount = m_CachedPlayerStatsComponent.GetPlayerCurrentManaPercentage();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(float damageToApply)
    {
        m_CachedPlayerStatsComponent.RemovePlayerHealth(damageToApply);
        if(m_CachedPlayerStatsComponent.GetPlayerCurrentHealth() <= 0f)
        {
            m_CachedPlayerComponent.PlayDeathSound();
            m_CachedPlayerComponent.PlayDeathAnimation();
        }
        else
        {
            m_CachedPlayerComponent.PlayDamageSound();
        }
    }
}
