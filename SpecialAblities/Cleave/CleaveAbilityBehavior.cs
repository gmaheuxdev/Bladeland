using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveAbilityBehavior : SpecialAbilityBehavior
{
    public override void ApplyAbilityEffect()
    {
        Vector3 center = m_AbilityOwner.transform.position;
        float radius = m_AbilityConfig.GetAbilityMaxRange();
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        DamageComponent abilityOwnerDamageComponent = m_AbilityOwner.GetComponent<DamageComponent>();

        foreach (Collider hitcol in hitColliders)
        {
            DamageComponent hitDamageComponent = hitcol.gameObject.GetComponent<DamageComponent>();
            
            if (hitDamageComponent != null && abilityOwnerDamageComponent != null &&
                hitDamageComponent.GetCurrentTeam() != abilityOwnerDamageComponent.GetCurrentTeam())
            {
                hitDamageComponent.TakeDamage((m_AbilityConfig as CleaveAbilityConfig).GetCleaveDamage());
            } 
        }
   }
}
