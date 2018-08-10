using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/SlowingShotAbility"))]
public class SlowingShotAbilityConfig : SpecialAbilityConfig
{
    //Member variables
    [SerializeField] ProjectileConfig m_AbilityProjectileConfig;
    
    //Getters and Setters
    public ProjectileConfig GetProjectileConfig() { return m_AbilityProjectileConfig;}
        
    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<SlowingShotAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<SlowingShotAbilityBehavior>());
    }
}
