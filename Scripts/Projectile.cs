using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float m_ProjectileDamageAmount;
    GameObject m_CachedPlayerGameObject;
    [SerializeField] float m_ProjectileSpeed;
    Vector3 m_TargetPosition;
    [SerializeField] private Vector3 m_AimOffset;
   
    // Use this for initialization
    void Start ()
    {
        m_CachedPlayerGameObject = GameObject.Find("Player");
        m_TargetPosition = m_CachedPlayerGameObject.transform.position + m_AimOffset;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 currentPosition = transform.position;
        
        Vector3 projectileToPlayerDirection = m_TargetPosition - currentPosition;
        projectileToPlayerDirection.Normalize();

        Vector3 newPosition = transform.position + projectileToPlayerDirection * m_ProjectileSpeed * Time.deltaTime;
        transform.position = newPosition;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));

        if (damageableComponent != null && collision.gameObject.layer != gameObject.layer)
        {
            (damageableComponent as IDamageable).TakeDamage(m_ProjectileDamageAmount);
            Destroy(gameObject);
        }
    }
}
