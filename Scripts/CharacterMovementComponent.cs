using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementComponent : MonoBehaviour
{
    //Member variables
    private NavMeshAgent m_CachedPlayerNavMeshAgent;
    private CharacterStatsComponent m_CachedPlayerStatsComponent;
    
    //Animation stuff
    [SerializeField]  float m_MovingTurnSpeed = 360;
    [SerializeField]  float m_StationaryTurnSpeed = 180;
    [SerializeField]  float m_MovingSpeedMultiplier;
    [SerializeField]  float m_AnimationSpeedMultiplier;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;

    NavMeshAgent GetCharacterNavMeshAgent() {return m_CachedPlayerNavMeshAgent;}

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CachedPlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        m_CachedPlayerStatsComponent = GetComponent<CharacterStatsComponent>();

        m_CachedPlayerNavMeshAgent.updateRotation = false;
        m_CachedPlayerNavMeshAgent.updatePosition = true;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (m_CachedPlayerNavMeshAgent.remainingDistance > m_CachedPlayerNavMeshAgent.stoppingDistance)
        {
            Move(m_CachedPlayerNavMeshAgent.desiredVelocity);
        }
        else
        {
            Move(Vector3.zero);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void BeginMoveTo(Vector3 newDestination)
    {
        m_CachedPlayerNavMeshAgent.SetDestination(newDestination);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        ApplyExtraTurnRotation();
        UpdateAnimator();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateAnimator()
    {
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.speed = m_AnimationSpeedMultiplier;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void OnAnimatorMove()//Callback
    {
        if (Time.deltaTime > 0)
        {
            Vector3 velocity = (m_Animator.deltaPosition * m_MovingSpeedMultiplier) / Time.deltaTime;
            velocity.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = velocity;
        }
    }

}//End class

