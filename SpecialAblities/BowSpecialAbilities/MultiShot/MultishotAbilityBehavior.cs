using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotAbilityBehavior : SpecialAbilityBehavior
{
    public override void ApplyAbilityEffect()
    {
        MultishotAbilityConfig multishotConfig = m_AbilityConfig as MultishotAbilityConfig;
        Vector3 projectileDirection = transform.forward;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        //1
        GameObject spawnedProjectile = Instantiate(multishotConfig.GetProjectileToSpawn(), spawnPosition, transform.rotation);
        ProjectileBehavior spawnedProjectilebehavior = spawnedProjectile.GetComponent<ProjectileBehavior>();
        
        //2
        Quaternion rotationQuat = CreateRotationQuat(10,transform.up);
        Quaternion finalQuat = rotationQuat * transform.rotation;
        GameObject spawnedProjectile2 = Instantiate(multishotConfig.GetProjectileToSpawn(), spawnPosition, finalQuat);
        ProjectileBehavior spawnedProjectilebehavior2 = spawnedProjectile2.GetComponent<ProjectileBehavior>();
        
        //3
        Quaternion rotationQuat1 = CreateRotationQuat(-10,transform.up);
        Quaternion finalQuat1 = rotationQuat1 * transform.rotation;
        GameObject spawnedProjectile3 = Instantiate(multishotConfig.GetProjectileToSpawn(), spawnPosition, finalQuat1);
        ProjectileBehavior spawnedProjectilebehavior3 = spawnedProjectile3.GetComponent<ProjectileBehavior>();
    }

    Quaternion CreateRotationQuat(float rotationAngle, Vector3 baseVector)
    {
        rotationAngle *= Mathf.Deg2Rad;

        float w = Mathf.Cos(rotationAngle / 2);
        float x = (baseVector.x) * Mathf.Sin(rotationAngle / 2);
        float y = (baseVector.y) * Mathf.Sin(rotationAngle / 2);
        float z = (baseVector.z) * Mathf.Sin(rotationAngle / 2);

        return new Quaternion(x, y, z, w);
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnMultiShotAnimationFinished()
    {
        m_AbilityOwnerAnimator.SetBool("IsDoSpecialAbility", false);
        m_AbilityOwnerAnimator.runtimeAnimatorController = m_AbilityOwnerWeaponComponent.GetActiveWeaponConfig().GetWeaponAnimatorOverride();
        ApplyAbilityEffect();
    }
}
