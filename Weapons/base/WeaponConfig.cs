using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponConfig : ScriptableObject
{
    //Member variables
    [Header("Weapon General")]
    [SerializeField] private GameObject m_WeaponPrefab;
    [SerializeField] private AnimationClip m_WeaponAttackAnimation;
    [SerializeField] private float m_WeaponAttackRange;
    [SerializeField] private GameObject m_EquippedWeaponPosRotPreset;
    [SerializeField] private float m_WeaponAttackDamage;
    [SerializeField] private float m_WeaponTimeBetweenAttacks;
    [SerializeField] private AudioClip m_WeaponAttackSound;
    [SerializeField] private AnimatorOverrideController m_WeaponAnimatorOverride;
    [SerializeField] private SpecialAbilityConfig[] m_WeaponSpecialAbilites;

    WeaponBehavior m_WeaponBehavior;

    //Abstract methods
    public abstract WeaponBehavior AttachWeaponBehaviorTo(GameObject gameObjectToAttachTo);
    public abstract void DetachWeaponBehavior(GameObject gameObjectToDetachFrom);

    //Getters and Setters
    public float GetWeaponTimeBetweenAttacks() { return m_WeaponTimeBetweenAttacks; }
    public float GetWeaponAttackRange() { return m_WeaponAttackRange; }
    public float GetWeaponAttackDamage() { return m_WeaponAttackDamage; }
    public GameObject GetWeaponPrefab() { return m_WeaponPrefab; }
    public AnimationClip GetWeaponAttackAnimation() { return m_WeaponAttackAnimation; }
    public GameObject GetEquippedWeaponPosRotPreset() { return m_EquippedWeaponPosRotPreset; }
    public AudioClip GetWeaponAttackSound() { return m_WeaponAttackSound; }
    public AnimatorOverrideController GetWeaponAnimatorOverride() { return m_WeaponAnimatorOverride; }
    public SpecialAbilityConfig[] GetWeaponSpecialAbilities() { return m_WeaponSpecialAbilites;}
    public WeaponBehavior GetWeaponBehavior() {return m_WeaponBehavior;}

    public void SetupWeaponConfig(GameObject gameObjectToAttachTo)
    {
        m_WeaponBehavior = AttachWeaponBehaviorTo(gameObjectToAttachTo);
        m_WeaponBehavior.SetWeaponConfig(this);
        m_WeaponBehavior.SetWeaponOwner(gameObjectToAttachTo);
    }

    public void UseWeapon()
    {
        m_WeaponBehavior.Use();
    }
}
