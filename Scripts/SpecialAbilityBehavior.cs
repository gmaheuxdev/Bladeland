using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbilityBehavior : MonoBehaviour
{
    protected SpecialAbilityConfig m_AbilityConfig;
    public abstract void Use(GameObject target); //TODO: BETTER SOLUTION?? want to enforce when needed and not enforce when not needed
    public void SetAbilityConfig(SpecialAbilityConfig newAbilityConfig){m_AbilityConfig = newAbilityConfig;}

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

    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void PlayAbilityAnimation()
    {

    }
}
