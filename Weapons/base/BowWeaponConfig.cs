using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("BowWeapon"))]
public class BowWeaponConfig : WeaponConfig
{
    [SerializeField] GameObject m_ProjectilePrefab;
    
    public GameObject GetProjectilePrefab() {return m_ProjectilePrefab;}

    public override WeaponBehavior AttachWeaponBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<BowWeaponBehavior>();
    }

    public override void DetachWeaponBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<BowWeaponBehavior>());
    }
}
