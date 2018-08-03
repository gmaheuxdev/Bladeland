using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbilityConfig : SpecialAbilityConfig
{
    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<HealAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<HealAbilityBehavior>());
    }
}
