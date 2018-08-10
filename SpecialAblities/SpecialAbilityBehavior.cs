using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SpecialAbilityBehavior : MonoBehaviour
{
    protected SpecialAbilityConfig m_AbilityConfig;
    protected GameObject m_AbilityOwner;
    protected Animator m_AbilityOwnerAnimator;
    protected GameObject m_AbilityCurrentTarget;

    public abstract void ApplyAbilityEffect();
    public void SetAbilityCurrentTarget(GameObject newTarget) { m_AbilityCurrentTarget = newTarget;}
    public void SetAbilityConfig(SpecialAbilityConfig newAbilityConfig) { m_AbilityConfig = newAbilityConfig; }
    public SpecialAbilityConfig GetAbilityConfig() { return m_AbilityConfig;}
    public void SetAbilityOwner(GameObject newAbilityOwner) { m_AbilityOwner = newAbilityOwner;}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    protected void Start()
    {
        m_AbilityOwnerAnimator = m_AbilityOwner.GetComponent<Animator>();
    }

    public void Use()
    {
        m_AbilityOwnerAnimator.SetBool("IsDoSpecialAbility", true);
        m_AbilityOwnerAnimator.runtimeAnimatorController = m_AbilityConfig.GetAbilityAnimationOverride();

        CameraRayCaster cameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        if ((int)cameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
           m_AbilityCurrentTarget = cameraRaycaster.GetCurrentActiveHit().collider.gameObject;
        }
    }

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

  
}
