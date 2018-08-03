using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/PowerAttack"))]
public class PowerAttackConfig : SpecialAbilityConfig
{
    [Header("PowerAttackValues")]
    [SerializeField] float m_PowerAttackDamage;

    public float GetPowerAttackDamage() { return m_PowerAttackDamage;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<PowerAttackBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<PowerAttackBehavior>());
    }
}
