using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBlastBehavior : SpecialAbilityBehavior
{
    //Cached Components
    ProjectileBehavior m_CachedProjectileBehavior;

    //Member Variables
    Vector3 m_ProjectileSpawnOffset = Vector3.up;


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void ApplyAbilityEffect()
    {
        SetupProjectile();
        PlayAbilitySounds();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SetupProjectile()
    {
        GameObject spawnedProjectile = Instantiate(m_AbilityConfig.GetProjectileToSpawn(), transform.position + m_ProjectileSpawnOffset, Quaternion.identity);

        m_CachedProjectileBehavior = spawnedProjectile.GetComponent<ProjectileBehavior>();
        m_CachedProjectileBehavior.SetProjectileDirection(transform.forward);
    }
}
