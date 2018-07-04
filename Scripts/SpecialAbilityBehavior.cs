using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbilityBehavior : MonoBehaviour
{
    protected SpecialAbilityConfig m_AbilityConfig;
    protected GameObject m_AbilityOwner;

    public abstract void Use(); //TODO: BETTER SOLUTION?? want to enforce when needed and not enforce when not needed
    public void SetAbilityConfig(SpecialAbilityConfig newAbilityConfig) { m_AbilityConfig = newAbilityConfig; }
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
    protected void PlayAbilitySounds()
    {
        m_AbilityOwner.GetComponent<AudioSource>().PlayOneShot(m_AbilityConfig.GetAbilitySound());
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PlayAbilityAnimation()
    {

    }
}
