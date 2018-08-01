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
    [SerializeField] AnimatorOverrideController m_AbilityAnimationOverride;
    [SerializeField] AnimationClip m_AbilityAnimation;
    [SerializeField] GameObject m_ProjectileToSpawn;
    [SerializeField] GameObject m_ZoneOfEffect;
    [SerializeField] float m_AbilityMaxRange;

    SpecialAbilityBehavior m_AbilityBehavior;
    
    //Getters and setters
    public float GetAbilityManaCost() { return m_AbilityManaCost; }
    public float GetAbilityMaxRange() { return m_AbilityMaxRange; }
    public GameObject GetAbilityParticleSystemPrefab() { return m_AbilityParticleSystemPrefab; }
    public AudioClip GetAbilitySound() { return m_AbilitySound; }
    public AnimatorOverrideController GetAbilityAnimationOverride() { return m_AbilityAnimationOverride; }
    public AnimationClip GetAbilityAnimation() { return m_AbilityAnimation;}
    public GameObject GetProjectileToSpawn() { return m_ProjectileToSpawn;}
 
    //Abstract methods
    public abstract SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo);

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetupAbility(GameObject gameObjectToAttachTo)
    {
        m_AbilityBehavior = AttachAbilityBehaviorTo(gameObjectToAttachTo);
        m_AbilityBehavior.SetAbilityConfig(this);
        m_AbilityBehavior.SetAbilityOwner(gameObjectToAttachTo);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UseSpecialAbility()
    {
        m_AbilityBehavior.Use();
    }
}
