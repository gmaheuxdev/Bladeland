using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("SpecialAbilites/AoeAttack"))]
public class AoeAttackConfig : SpecialAbilityConfig
{
    [SerializeField] float m_AoeRadius;
    [SerializeField] float m_AoeDamage;
    
    public float GetAoeRadius() {return m_AoeRadius;}
    public float GetAoeDamage() {return m_AoeDamage;}
    
    public override SpecialAbilityBehavior AttachAbilityBehaviorTo(GameObject gameObjectToAttachTo)
    {
        return gameObjectToAttachTo.AddComponent<AoeAttackBehavior>();
    }
}
