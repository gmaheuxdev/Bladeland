using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour, IDamageable
{
    GameObject m_CachedPlayerGameObject;
    PlayerStatsComponent m_CachedPlayerStatsComponent;

    void Start()
    {
        m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player"); //Later replace with a helper method to find player
        m_CachedPlayerStatsComponent = m_CachedPlayerGameObject.GetComponent<PlayerStatsComponent>();
    }

    public void TakeDamage(float damageToApply)
    {
        m_CachedPlayerStatsComponent.RemovePlayerHealth(damageToApply);
    }
}
