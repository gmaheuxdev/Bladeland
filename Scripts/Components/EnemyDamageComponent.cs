using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageComponent : MonoBehaviour, IDamageable
{
    EnemyStatsComponent m_CachedEnemyStatsComponent;

    private void Start()
    {
        m_CachedEnemyStatsComponent = gameObject.GetComponent<EnemyStatsComponent>();
    }

    public void TakeDamage(float damage)
    {
        m_CachedEnemyStatsComponent.RemoveEnemyHealth(damage);
    }

    internal void TakeDamage(object m_playerDamage)
    {
        throw new NotImplementedException();
    }
}
