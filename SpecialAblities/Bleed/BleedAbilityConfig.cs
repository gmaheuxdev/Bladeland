using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/BleedAbility"))]
public class BleedAbilityConfig : SpecialAbilityConfig
{
    [Header("BleedValues")]
    [SerializeField]  float m_TimeBetweenTicks;
    [SerializeField]  float m_DamagePerTick;
    [SerializeField]  int m_TickAmount;

    public int GetTickAmount() { return m_TickAmount; }
    public float GetDamagePerTick() { return m_DamagePerTick; }
    public float GetTimeBetweenTicks() { return m_TimeBetweenTicks;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<BleedAbilityBehavior>();
    }
}
