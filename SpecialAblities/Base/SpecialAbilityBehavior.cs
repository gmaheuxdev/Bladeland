using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbilityBehavior : MonoBehaviour
{
    protected SpecialAbilityConfig m_AbilityConfig;
    protected GameObject m_AbilityOwner;
    protected Animator m_AbilityOwnerAnimator;
    
    public abstract void Use();
    public abstract void ApplyAbilityEffect();
    public void SetAbilityConfig(SpecialAbilityConfig newAbilityConfig) { m_AbilityConfig = newAbilityConfig; }
    public SpecialAbilityConfig GetAbilityConfig() { return m_AbilityConfig;}
    public void SetAbilityOwner(GameObject newAbilityOwner) { m_AbilityOwner = newAbilityOwner;}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PlayAbilityEffects()
    {
        GameObject particleSystemPrefab = m_AbilityConfig.GetAbilityParticleSystemPrefab();
        var particleSystemInstance = Instantiate(m_AbilityConfig.GetAbilityParticleSystemPrefab(),transform.position, Quaternion.identity);
        particleSystemInstance.GetComponent<ParticleSystem>().Play();
        Destroy(particleSystemInstance, 5); //TODO:CHANGE MAGIC NUMBER
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void PlayAbilitySounds()
    {
        m_AbilityOwner.GetComponent<AudioSource>().PlayOneShot(m_AbilityConfig.GetAbilitySound());
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void SetAbilityAnimationOverride(AnimatorOverrideController abilityOverrideController)
    {
        m_AbilityOwnerAnimator = m_AbilityOwner.GetComponent<Animator>();
        m_AbilityOwnerAnimator.runtimeAnimatorController = abilityOverrideController;
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnMeleeStunAbilityAnimationFinished()
    {
        m_AbilityOwner.GetComponent<Animator>().SetBool("IsDoSpecialAbility", false);
        ApplyAbilityEffect();
    }

}
