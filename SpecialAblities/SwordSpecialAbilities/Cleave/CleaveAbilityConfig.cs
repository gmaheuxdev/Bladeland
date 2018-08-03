using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/CleaveAbility"))]
public class CleaveAbilityConfig : SpecialAbilityConfig
{
    [Header("CleaveValues")]
    [SerializeField] float m_CleaveDamage;

    public float GetCleaveDamage() { return m_CleaveDamage;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<CleaveAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<CleaveAbilityBehavior>());
    }
}
