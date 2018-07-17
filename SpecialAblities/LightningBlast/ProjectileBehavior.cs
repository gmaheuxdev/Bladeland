using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    //Member variables
    Vector3 m_ProjectileDirection;
    [SerializeField] float m_ProjectileSpeed;
    [SerializeField] float m_DestroyTimer;
    CharacterTeamEnum m_ProjectileTeam;
    
    //Getters and setters
    public void SetProjectileDirection(Vector3 newDirection) {m_ProjectileDirection = newDirection;}
   
	void Update ()
    {
        Vector3 velocity = m_ProjectileDirection * m_ProjectileSpeed * Time.deltaTime;
        transform.position +=velocity;

        if(m_DestroyTimer <=0)
        {
            Destroy(gameObject);
        }
        else
        {
            m_DestroyTimer -= Time.deltaTime;
        }
    }
}
