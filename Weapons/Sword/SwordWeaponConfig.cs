using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SwordWeapon"))]
public class SwordWeaponConfig : WeaponConfig
{
    public override WeaponBehavior AttachWeaponBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<SwordWeaponBehavior>();
    }

    public override void DetachWeaponBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<SwordWeaponBehavior>());
    }
}
