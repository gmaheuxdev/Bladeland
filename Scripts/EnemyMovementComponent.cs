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

        [SerializeField] private Transform m_MovementTargetTransform;
        [SerializeField] private float m_AttackMinDistance;
       
        
        //Getters and setters
        public void SetMovementTargetTransform(Transform newTargetTransform){m_MovementTargetTransform = newTargetTransform;}
        

        ////////////////////////////////////////////////////////////
        void Start()
        {
            m_CachedNavMeshAgent = GetComponent<NavMeshAgent>();
            m_CachedThirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
            m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player");

            m_CachedNavMeshAgent.updateRotation = false;
            m_CachedNavMeshAgent.updatePosition = true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update()
        {
            UpdateMovementTarget();
            UpdateMovement();
        }
                
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UpdateMovementTarget()
        {
            float distanceToPlayer = Vector3.Distance(m_CachedPlayerGameObject.transform.position, transform.position);

            if (distanceToPlayer <= m_AttackMinDistance)
            {
                m_MovementTargetTransform = m_CachedPlayerGameObject.transform;
            }
            else
            {
                m_MovementTargetTransform = transform;
            }
        }
        
        //////////////////////////////////////////////////////////////////////////
        private void UpdateMovement()
        {
            //Update Target position
            if (m_MovementTargetTransform != null)
            {
                m_CachedNavMeshAgent.SetDestination(m_MovementTargetTransform.position);
            }

            //Move towards if in movement range
            if (m_CachedNavMeshAgent.remainingDistance > m_CachedNavMeshAgent.stoppingDistance)
            {
                m_CachedThirdPersonCharacter.Move(m_CachedNavMeshAgent.desiredVelocity, false, false);
            }
            else
            {
                m_CachedThirdPersonCharacter.Move(Vector3.zero, false, false);
            }
        }
    }
}
