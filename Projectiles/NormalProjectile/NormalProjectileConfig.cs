using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Projectiles/NormalProjectile"))]
public class NormalProjectileConfig : ProjectileConfig
{
    public override ProjectileBehavior AttachProjectileBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<NormalProjectileBehavior>();
    }
}
