using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingShotAbilityBehavior : SpecialAbilityBehavior
{
    public override void ApplyAbilityEffect()
    {
        ProjectileConfig projectileConfig = (m_AbilityConfig as SlowingShotAbilityConfig).GetProjectileConfig();
        
        //REMEMBER TO REMOVE GETPROJECTILETOSPAWN
        GameObject spawnedProjectile = Instantiate(projectileConfig.GetProjectilePrefab(), transform.position,transform.rotation);
        projectileConfig.SetupProjectile(spawnedProjectile);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnSlowingShotAnimationFinished()
    {
        m_AbilityOwnerAnimator.SetBool("IsDoSpecialAbility", false);
        m_AbilityOwnerAnimator.runtimeAnimatorController = m_AbilityOwnerWeaponComponent.GetActiveWeaponConfig().GetWeaponAnimatorOverride();
        ApplyAbilityEffect();
    }
}
