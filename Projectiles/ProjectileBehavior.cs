using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBehavior : MonoBehaviour
{
    //Member variables
    ProjectileConfig m_ProjectileConfig;
    [SerializeField] float m_ProjectileSpeed;
    CharacterTeamEnum m_ProjectileTeam;

    //Getters and Setters
    ProjectileConfig GetProjectileConfig() { return m_ProjectileConfig;}
    public void SetProjectileConfig(ProjectileConfig newProjectileConfig) { m_ProjectileConfig = newProjectileConfig;}
        
    void Update()
    {
        Vector3 velocity = gameObject.transform.forward * m_ProjectileSpeed * Time.deltaTime;
        transform.position += velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        DamageComponent otherDamageComponent;

        otherDamageComponent = collision.gameObject.GetComponent<DamageComponent>();
        if (otherDamageComponent != null && otherDamageComponent.GetCurrentTeam() == CharacterTeamEnum.CharacterTeamEnum_AI)
        {
            Destroy(gameObject);
        }
    }
}
