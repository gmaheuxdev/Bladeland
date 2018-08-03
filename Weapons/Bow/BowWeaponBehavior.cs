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
        BowWeaponConfig weaponConfig = m_WeaponConfig as BowWeaponConfig;
        GameObject spawnedProjectile = Instantiate(weaponConfig.GetProjectilePrefab(), transform.position, Quaternion.identity);
        ProjectileBehavior spawnedProjectilebehavior = spawnedProjectile.GetComponent<ProjectileBehavior>();
        spawnedProjectilebehavior.SetProjectileDirection(transform.forward);
        
    }
}
