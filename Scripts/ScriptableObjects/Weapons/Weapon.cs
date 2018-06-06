using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("Weapon"))]
public class Weapon : ScriptableObject
{
    [SerializeField] private GameObject m_WeaponPrefab;
    [SerializeField] private AnimationClip m_WeaponAttackAnimation;
    [SerializeField] private float m_WeaponAttackRange;
    [SerializeField] private GameObject m_EquippedWeaponPosRotPreset;


    public float GetWeaponAttackRange()
    {
        return m_WeaponAttackRange;
    }
    
    public GameObject GetWeaponPrefab()
    {
        return m_WeaponPrefab;
    }

    public AnimationClip GetWeaponAttackAnimation()
    {
        return m_WeaponAttackAnimation;
    }


    public GameObject GetEquippedWeaponPosRotPreset()
    {
        return m_EquippedWeaponPosRotPreset;
    }

    public void ClearAnimationEvents()
    {
        m_WeaponAttackAnimation.events = new AnimationEvent[0];
    }

}
