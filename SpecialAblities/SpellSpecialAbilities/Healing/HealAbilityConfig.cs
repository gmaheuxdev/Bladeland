using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/HealTargetAbility"))]
public class HealAbilityConfig : SpecialAbilityConfig
{
    [SerializeField] float m_HealAmount;

    //Getters and Setters
    public float GetHealAmount() {return m_HealAmount;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<HealAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<HealAbilityBehavior>());
    }
}
