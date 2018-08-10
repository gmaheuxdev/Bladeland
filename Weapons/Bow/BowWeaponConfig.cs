using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("BowWeapon"))]
public class BowWeaponConfig : WeaponConfig
{
    [SerializeField] ProjectileConfig m_BowProjectileConfig;
    
    public ProjectileConfig GetProjectileConfig() {return m_BowProjectileConfig;}

    public override WeaponBehavior AttachWeaponBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<BowWeaponBehavior>();
    }

    public override void DetachWeaponBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<BowWeaponBehavior>());
    }
}
