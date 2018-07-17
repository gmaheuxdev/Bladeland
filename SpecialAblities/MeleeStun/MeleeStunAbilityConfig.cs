using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStunAbilityConfig : SpecialAbilityConfig
{
    [SerializeField] float m_AbilityEffectDuration;

    //Getters and setters
    public float GetAbilityEffectDuration() {return m_AbilityEffectDuration;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<MeleeStunAbilityBehavior>();
    }
}
