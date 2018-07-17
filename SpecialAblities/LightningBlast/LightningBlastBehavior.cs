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
    public override void Use() //optional target
    {
        SetAbilityAnimationOverride(m_AbilityConfig.GetAbilityAnimationOverride());
        m_AbilityOwner.GetComponent<Animator>().SetBool("IsDoSpecialAbility", true);
    }

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

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnLightBlastAnimationFinished()
    {
        m_AbilityOwner.GetComponent<Animator>().SetBool("IsDoSpecialAbility", false);
        m_AbilityOwner.GetComponent<LightningBlastBehavior>().ApplyAbilityEffect();
    }
}
