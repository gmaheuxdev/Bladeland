using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileConfig : ScriptableObject
{
    //Member variables
    ProjectileBehavior m_ProjectileBehavior;
    [SerializeField] private GameObject m_ProjectilePrefab;
    [SerializeField] private float m_ProjectileTimeAlive;
    [SerializeField] private float m_ProjectileDamage;
    [SerializeField] private AudioClip m_ProjectileHitSound;

    //Getters and Setters
    public GameObject GetProjectilePrefab() {return m_ProjectilePrefab;}
    public float GetProjectileTimeAlive() {return m_ProjectileTimeAlive;}
    public float GetProjectileDamage() { return m_ProjectileDamage;}
    public AudioClip GetProjectileHitsound() {return m_ProjectileHitSound;}
    
    //Abstract methods
    public abstract ProjectileBehavior AttachProjectileBehaviorTo(GameObject gameObjectToAttachTo);

    public void SetupProjectile(GameObject gameObjectToAttachTo)
    {
        m_ProjectileBehavior = AttachProjectileBehaviorTo(gameObjectToAttachTo);
        m_ProjectileBehavior.SetProjectileConfig(this);
    }
}
