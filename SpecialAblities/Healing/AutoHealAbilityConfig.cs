using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/AutoHeal"))]
public class AutoHealAbilityConfig : SpecialAbilityConfig
{
    [Header("PowerAttackValues")]
    [SerializeField] float m_HealingAmount;

    public float GetHealingAmount() {return m_HealingAmount;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<AutoHealAbilityBehavior>();
    }
}