using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowWeaponBehavior : WeaponBehavior
{
    void OnBowAttackAnimationFinished()
    {
        m_WeaponOwnerAnimator.SetBool("IsAttacking", false);
        DoWeaponBehavior();
    }

    protected override void DoWeaponBehavior()
    {
        BowWeaponConfig bowConfig = m_WeaponConfig as BowWeaponConfig;
        ProjectileConfig bowProjectileConfig = bowConfig.GetProjectileConfig();

        GameObject spawnedProjectile = Instantiate(bowProjectileConfig.GetProjectilePrefab(), transform.position, m_WeaponOwner.transform.rotation);
    }
}
