using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/Slow"))]
public class SlowAbilityConfig : SpecialAbilityConfig
{
    [Header("SlowAbilityValues")]
    [SerializeField] float m_AbilityEffectRadius;
    [SerializeField] float m_AbilityDuration;
    [SerializeField] float m_AbilitySlowPercentage;
    [SerializeField] float m_AbilityTimeBeforeActivation;

    //Getters and setters
    public float GetAbilityEffectRadius() {return m_AbilityEffectRadius;}
    public float GetAbilityTimeBeforeActivation() { return m_AbilityTimeBeforeActivation;}
    public float GetAbilityDuration() {return m_AbilityDuration;}
    public float GetAbilitySlowPercentage() {return m_AbilitySlowPercentage;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<SlowAbilityBehavior>();
    }
}
