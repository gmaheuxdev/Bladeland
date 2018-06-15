using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbilityConfig : ScriptableObject
{
    //Member variables
    [Header("Special Ability General")]
    [SerializeField] float m_AbilityManaCost;
    [SerializeField] GameObject m_AbilityParticleSystemPrefab;
    [SerializeField] AudioClip m_AbilitySound;
    [SerializeField] AnimationClip m_AbilityAnimation;
    protected SpecialAbilityBehavior m_AbilityBehavior;

    //Getters and setters
    public float GetAbilityManaCost() { return m_AbilityManaCost; }
    public GameObject GetAbilityParticleSystemPrefab() { return m_AbilityParticleSystemPrefab; }
    public AudioClip GetAbilitySound() { return m_AbilitySound; }
    public AnimationClip GetAbilityAnimation() { return m_AbilityAnimation;}

    //Abstract methods
    public abstract SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo);

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetupAbility(GameObject gameObjectToAttachTo)
    {
        m_AbilityBehavior = AttachAbilityBehaviorTo(gameObjectToAttachTo);
        m_AbilityBehavior.SetAbilityConfig(this);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UseSpecialAbility(GameObject target)
    {
        m_AbilityBehavior.Use(target);
    }
}
