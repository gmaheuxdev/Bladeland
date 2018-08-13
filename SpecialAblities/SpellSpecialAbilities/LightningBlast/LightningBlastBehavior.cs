using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBlastBehavior : SpecialAbilityBehavior
{
    //Member Variables
    Vector3 m_ProjectileSpawnOffset = Vector3.up;


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void ApplyAbilityEffect()
    {
        SetupProjectile();
       // PlayAbilitySounds();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SetupProjectile()
    {
        ProjectileConfig blastProjectileConfig = (m_AbilityConfig as LightningBlastConfig).GetBlastProjectileConfig();
        GameObject spawnedProjectile = Instantiate(blastProjectileConfig.GetProjectilePrefab(), transform.position + m_ProjectileSpawnOffset, gameObject.transform.rotation);
    }

    void OnLightningBlastAnimationFinished()
    {
        m_AbilityOwnerAnimator.SetBool("IsDoSpecialAbility", false);
        m_AbilityOwnerAnimator.runtimeAnimatorController = m_AbilityOwnerWeaponComponent.GetActiveWeaponConfig().GetWeaponAnimatorOverride();
        ApplyAbilityEffect();
    }
}
