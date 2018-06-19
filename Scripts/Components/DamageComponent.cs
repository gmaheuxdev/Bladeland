using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageComponent : MonoBehaviour
{
    //Member variables
    CharacterStatsComponent m_CachedStatsComponent;
    [SerializeField] private Image m_HealthBar;
    [SerializeField] private Image m_ManaBar;
    
    //////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_CachedStatsComponent = gameObject.GetComponent<CharacterStatsComponent>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if(m_HealthBar && m_ManaBar)
        {
            m_HealthBar.fillAmount = m_CachedStatsComponent.GetCurrentHealthPercentage();
            m_ManaBar.fillAmount = m_CachedStatsComponent.GetCurrentManaPercentage();
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(float damageToApply)
    {
        m_CachedStatsComponent.RemoveHealth(damageToApply);
        if(m_CachedStatsComponent.GetCurrentHealth() <= 0f)
        {
            PlayDeathSound();
            PlayDeathAnimation();
        }
        else
        {
           PlayDamageSound();
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private float RollCriticalHit()
    {
        /* float critRoll = UnityEngine.Random.Range(0f, 1f);
         float damageToApply = m_playerDamage;

         if (critRoll < m_CachedPlayerStatsComponent.GetCritChance())
         {
             damageToApply *= m_CachedPlayerStatsComponent.GetCritMultiplier();
         }

         return damageToApply;
     */
        return 0;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayDamageSound()
    {
        print("PlayDamageSound");
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayDeathAnimation()
    {
        print("PlayDeathAnimation");
    }

    //////////////////////////////////////////////////////////////////////////////////////////////
    private void PlayDeathSound()
    {
        print("PlayDeathSound");
    }
}
