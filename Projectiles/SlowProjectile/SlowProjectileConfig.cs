using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Projectiles/SlowProjectile"))]
public class SlowProjectileConfig : ProjectileConfig
{
    public override ProjectileBehavior AttachProjectileBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<SlowProjectileBehavior>();
    }
}
