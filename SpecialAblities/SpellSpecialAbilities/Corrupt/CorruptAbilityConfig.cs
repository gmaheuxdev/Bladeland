using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptAbilityConfig : SpecialAbilityConfig
{
    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<CorruptAbilityBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<CorruptAbilityBehavior>());
    }
}
