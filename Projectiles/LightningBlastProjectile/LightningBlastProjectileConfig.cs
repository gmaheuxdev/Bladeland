using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Projectiles/LightningBlastProjectile"))]
public class LightningBlastProjectileConfig : ProjectileConfig
{
    public override ProjectileBehavior AttachProjectileBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<LightningBlastProjectileBehavior>();
    }
}
