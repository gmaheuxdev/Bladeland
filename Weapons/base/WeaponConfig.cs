using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Weapon"))]
public class WeaponConfig : ScriptableObject
{
    //Member variables
    [SerializeField] private GameObject m_WeaponPrefab;
    [SerializeField] private AnimationClip m_WeaponAttackAnimation;
    [SerializeField] private float m_WeaponAttackRange;
    [SerializeField] private GameObject m_EquippedWeaponPosRotPreset;
    [SerializeField] private float m_WeaponAttackDamage;
    [SerializeField] private float m_WeaponTimeBetweenAttacks;
    [SerializeField] private AudioClip m_WeaponAttackSound;
    [SerializeField] private AnimatorOverrideController m_WeaponAnimatorOverride;

    //Getters and Setters
    public float GetWeaponTimeBetweenAttacks() {return m_WeaponTimeBetweenAttacks;}
    public float GetWeaponAttackRange() {return m_WeaponAttackRange;}
    public float GetWeaponAttackDamage(){return m_WeaponAttackDamage;}
    public GameObject GetWeaponPrefab(){return m_WeaponPrefab;}
    public AnimationClip GetWeaponAttackAnimation(){return m_WeaponAttackAnimation;}
    public GameObject GetEquippedWeaponPosRotPreset(){return m_EquippedWeaponPosRotPreset;}
    public AudioClip GetWeaponAttackSound() {return m_WeaponAttackSound;}
    public AnimatorOverrideController GetWeaponAnimatorOverride() { return m_WeaponAnimatorOverride;}
    
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ClearAnimationEvents() //Prevents unwanted events baked into the assets #FreeAssetsProblems
    {
        m_WeaponAttackAnimation.events = new AnimationEvent[0];
    }
}
