using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct Timer
{
   public float m_TimeLeft;
   public float m_StartTime;
   private bool m_IsStarted;
   private bool m_IsFinished;

    public bool IsFinished()
    {
        if (m_IsStarted && m_TimeLeft <= 0) { return true;}
        return false;
    } 

    public bool IsStarted(){return m_IsStarted;}
    public void StopTimer() { m_IsStarted = false;}
    public void StartTimer(float duration) {m_IsStarted = true; m_StartTime = duration; m_TimeLeft = m_StartTime;}
};

public class CharacterMovementComponent : MonoBehaviour
{
    //Serialized variables
    [SerializeField] float m_MovingTurnSpeed;
    [SerializeField] float m_StationaryTurnSpeed;
    [SerializeField] float m_MovingSpeedMultiplier;
    [SerializeField] float m_AnimationSpeedMultiplier;
    
    //Cached Components
    private NavMeshAgent m_CachedPlayerNavMeshAgent;
    private Animator m_CachedAnimator;
    
    //Other member variables
    Rigidbody m_Rigidbody;
    float m_TurnAmount;
    float m_ForwardAmount;
    Timer m_MovementSpeedModifierTimer;
    
    
    //movementSpeedModifierstuff...clean that
    float m_BaseNavAgentMaxSpeed;
    float m_BaseAnimatorSpeed;

    //Getters and Setters
    NavMeshAgent GetCharacterNavMeshAgent() {return m_CachedPlayerNavMeshAgent;}

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CachedPlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        m_CachedAnimator = GetComponent<Animator>();

        m_BaseAnimatorSpeed = m_CachedAnimator.speed;
        m_BaseNavAgentMaxSpeed = m_CachedPlayerNavMeshAgent.speed;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (m_CachedPlayerNavMeshAgent.remainingDistance > m_CachedPlayerNavMeshAgent.stoppingDistance)
        {
            Move(m_CachedPlayerNavMeshAgent.desiredVelocity);
        }
        else
        {
            StopMovement();
        }

        if (m_MovementSpeedModifierTimer.IsStarted())
        {
            m_MovementSpeedModifierTimer.m_TimeLeft -= Time.deltaTime;

            if(m_MovementSpeedModifierTimer.IsFinished())
            {
                m_MovementSpeedModifierTimer.StopTimer();
                m_CachedAnimator.speed = m_BaseAnimatorSpeed;
                m_CachedPlayerNavMeshAgent.speed = m_BaseNavAgentMaxSpeed;
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void StopMovement()
    {
        m_CachedAnimator.SetBool("IsRunning", false);
        Move(Vector3.zero);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void BeginMoveTo(Vector3 newDestination)
    {
        m_CachedPlayerNavMeshAgent.SetDestination(newDestination);
        m_CachedAnimator.SetBool("IsRunning",true);
   }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) //Don't normalize when staying still
        {
            move.Normalize();
        }
        
        move = transform.InverseTransformDirection(move);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        ApplyExtraTurnRotation();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void OnAnimatorMove()//Callback
    {
        if (Time.deltaTime > 0)
        {
            Vector3 velocity = (m_CachedAnimator.deltaPosition * m_MovingSpeedMultiplier) / Time.deltaTime;
            velocity.y = m_Rigidbody.velocity.y;
            m_Rigidbody.velocity = velocity;
        }
    }

    public void ApplyMovementSpeedModifier(float percentage,float duration)
    {
        m_CachedPlayerNavMeshAgent.speed *= percentage;
        m_CachedAnimator.speed *= percentage;
        m_MovementSpeedModifierTimer.StartTimer(duration);
    }

}//End class

