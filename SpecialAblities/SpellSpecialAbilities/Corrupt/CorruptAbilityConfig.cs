using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/CorruptAbility"))]
public class CorruptAbilityConfig : SpecialAbilityConfig
{
    [SerializeField] int m_TickAmount;
    [SerializeField] float m_TimeBetweenTicks;

    public int GetTickAmount() {return m_TickAmount;}
    public float GetTimeBetweenTicks() { return m_TimeBetweenTicks;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<CorruptAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<CorruptAbilityBehavior>());
    }
}
