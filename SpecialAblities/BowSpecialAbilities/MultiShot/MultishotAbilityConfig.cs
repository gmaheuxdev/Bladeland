using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/MultishotAbility"))]
public class MultishotAbilityConfig : SpecialAbilityConfig
{
    //Serialized variables
    [SerializeField] int m_ProjectileAmount;
    [SerializeField] float m_AngleOffset;

   public int GetProjectileAmount() { return m_ProjectileAmount;}
   public float GetAngleOffset() {return m_AngleOffset;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<MultishotAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<MultishotAbilityBehavior>());
    }
}
