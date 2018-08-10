using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/FastAttackAbility"))]
public class FastAttackAbilityConfig : SpecialAbilityConfig
{
    [SerializeField] float m_TimeActive;

    public float GetTimeActive() {return m_TimeActive;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<FastAttackAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<FastAttackAbilityBehavior>());
    }
}
