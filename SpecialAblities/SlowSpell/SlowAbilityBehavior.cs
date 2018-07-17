using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAbilityBehavior : SpecialAbilityBehavior
{
    //Member Variables
    GameObject m_AbilityZoneOfEffect;
    GameObject m_CachedPlayerGameObject;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player");//Replace with static helper?
    }
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void Use()
    {
        SetAbilityAnimationOverride(m_AbilityConfig.GetAbilityAnimationOverride());
        m_AbilityOwner.GetComponent<Animator>().SetBool("IsDoSpecialAbility", true);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    public override void ApplyAbilityEffect()
    {
        SpawnAbilityZoneOfEffect();
        m_AbilityZoneOfEffect.GetComponent<CircleZoneBehavior>().SetOwnerGameObject(m_AbilityOwner);
        m_AbilityZoneOfEffect.GetComponent<CircleZoneBehavior>().StartActivationTimer((m_AbilityConfig as SlowAbilityConfig).GetAbilityTimeBeforeActivation());
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SpawnAbilityZoneOfEffect()
    {
        m_AbilityZoneOfEffect = Instantiate(m_AbilityConfig.GetZoneOfEffect(), m_CachedPlayerGameObject.transform.position, Quaternion.identity);
        Vector3 newScale = new Vector3((m_AbilityConfig as SlowAbilityConfig).GetAbilityEffectRadius() * 2,
                                        m_AbilityZoneOfEffect.transform.localScale.y,
                                        (m_AbilityConfig as SlowAbilityConfig).GetAbilityEffectRadius() * 2);

        m_AbilityZoneOfEffect.transform.localScale = newScale;
   }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnSlowAbilityAnimationFinished()
    {
        m_AbilityOwner.GetComponent<Animator>().SetBool("IsDoSpecialAbility", false);
        m_AbilityOwner.GetComponent<SlowAbilityBehavior>().ApplyAbilityEffect();
    }
}
