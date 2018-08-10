using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("FistsWeapon"))]
public class FistsWeaponConfig : WeaponConfig
{
    public override WeaponBehavior AttachWeaponBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<FistsWeaponBehavior>();
    }

    public override void DetachWeaponBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<FistsWeaponBehavior>());
    }
}
