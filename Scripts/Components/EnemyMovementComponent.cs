using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class EnemyMovementComponent : MonoBehaviour
    {
        //Member variables and cached components
        private NavMeshAgent m_CachedNavMeshAgent;
        private ThirdPersonCharacter m_CachedThirdPersonCharacter;
        private GameObject m_CachedPlayerGameObject;
        bool m_IsAttacking = false;

        [SerializeField] private float m_AggroRange;
        [SerializeField] private float m_AttackRange;
        [SerializeField] private GameObject m_ProjectileToSpawn;
        [SerializeField] private GameObject m_ProjectileSpawnLocation;


        ////////////////////////////////////////////////////////////
        void Start()
        {
            m_CachedNavMeshAgent = GetComponent<NavMeshAgent>();
            m_CachedThirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player");

            m_CachedNavMeshAgent.updateRotation = false;
            m_CachedNavMeshAgent.updatePosition = true;
            m_CachedNavMeshAgent.stoppingDistance = m_AttackRange;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update()
        {
            UpdateMovement();
        }

        //////////////////////////////////////////////////////////////////////////
        private void UpdateMovement()
        {
            float distanceToPlayer = Vector3.Distance(m_CachedPlayerGameObject.transform.position, gameObject.transform.position);
            
            //Move to Player when in aggro range
            if (distanceToPlayer < m_AggroRange)
            {
                m_CachedNavMeshAgent.SetDestination(m_CachedPlayerGameObject.transform.position);

                //Stop moving and attack when in attack range
                if (m_CachedNavMeshAgent.remainingDistance < m_AttackRange)
                {
                    if(!m_IsAttacking)
                    {
                        m_IsAttacking = true;
                        InvokeRepeating("SpawnProjectile", 0f, 1f);
                    }
                    m_CachedThirdPersonCharacter.Move(Vector3.zero, false, false);
                }
                else
                {
                    m_IsAttacking = false;
                    CancelInvoke();
                    m_CachedThirdPersonCharacter.Move(m_CachedNavMeshAgent.desiredVelocity, false, false);
                }
            }
            else //Don't move, the player is too far
            {
                m_CachedNavMeshAgent.SetDestination(transform.position);
                m_CachedThirdPersonCharacter.Move(Vector3.zero, false, false);
            }
        }

        void SpawnProjectile()
        {
            GameObject newProjectile = Instantiate(m_ProjectileToSpawn, m_ProjectileSpawnLocation.transform.position,Quaternion.identity);
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, m_AggroRange);
            Gizmos.DrawWireSphere(transform.position, m_AttackRange);
        }

    }//Class End
}//Namespace End
