﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/LightningBlast"))]
public class LightningBlastConfig : SpecialAbilityConfig
{
    [Header("LightningBlastValues")]
    [SerializeField] float m_DamageAmount;
    [SerializeField] ProjectileConfig m_BlastProjectileConfig;

    public float GetDamageAmount() {return m_DamageAmount;}
    public ProjectileConfig GetBlastProjectileConfig() {return m_BlastProjectileConfig;}

    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<LightningBlastBehavior>();
    }

    public override void DetachAbilityBehavior(GameObject gameObjectToDetachFrom)
    {
        Destroy(gameObjectToDetachFrom.GetComponent<LightningBlastBehavior>());
    }
}
