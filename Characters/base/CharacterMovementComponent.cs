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
     //Cached Components
    private NavMeshAgent m_CachedPlayerNavMeshAgent;
    private Animator m_CachedAnimator;
    
    //Other member variables
    Timer m_MovementSpeedModifierTimer;
    Timer m_AttackSpeedModifierTimer;
    private bool m_IsAimMode;
        
    //movementSpeedModifierstuff...clean that
    float m_BaseNavAgentMaxSpeed;
    float m_BaseAnimatorSpeed;
    Vector3 m_AimModeTargetPosition;

    //Getters and Setters
    public void SetIsAimMode(bool newValue) { m_IsAimMode = newValue;}

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_CachedPlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        m_CachedAnimator = GetComponent<Animator>();
        m_BaseNavAgentMaxSpeed = m_CachedPlayerNavMeshAgent.speed;
        m_IsAimMode = false;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        UpdateTimers();

        //Optimisation : make it an event instead?
        if (m_CachedPlayerNavMeshAgent.remainingDistance <=0 && m_CachedAnimator.GetBool("IsRunning")) 
        {
            StopMovement();
        }

        if(m_IsAimMode)
        {
            Vector3 targetDirection = m_AimModeTargetPosition - transform.position;
            float angularSpeed = 5 * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, angularSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateTimers()
    {
        //Movement speed timer
        if (m_MovementSpeedModifierTimer.IsStarted())
        {
            m_MovementSpeedModifierTimer.m_TimeLeft -= Time.deltaTime;

            if (m_MovementSpeedModifierTimer.IsFinished())
            {
                m_MovementSpeedModifierTimer.StopTimer();
                m_CachedPlayerNavMeshAgent.speed = m_BaseNavAgentMaxSpeed;
            }
        }

        //AttackSpeed timer
        if (m_AttackSpeedModifierTimer.IsStarted())
        {
            m_AttackSpeedModifierTimer.m_TimeLeft -= Time.deltaTime;

            if (m_AttackSpeedModifierTimer.IsFinished())
            {
                m_AttackSpeedModifierTimer.StopTimer();
                m_CachedAnimator.SetFloat("NormalAttackSpeedModifier", 1f);
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void StopMovement()
    {
        m_CachedAnimator.SetBool("IsRunning", false);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void BeginMoveTo(Vector3 newDestination)
    {
        m_CachedPlayerNavMeshAgent.SetDestination(newDestination);
        m_CachedAnimator.SetBool("IsRunning",true);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void BeginRotateTo(Vector3 targetPosition)
    {
        m_AimModeTargetPosition = targetPosition;
        m_IsAimMode = true;     
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ApplyMovementSpeedModifier(float percentage,float duration)
    {
        m_CachedPlayerNavMeshAgent.speed *= percentage;
        m_CachedAnimator.speed *= percentage;
        m_MovementSpeedModifierTimer.StartTimer(duration);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void ApplyAttackSpeedModifier(float timeActive)
    {
        m_CachedAnimator.SetFloat("NormalAttackSpeedModifier", 2f);
        m_AttackSpeedModifierTimer.StartTimer(timeActive);
    }


}//End class

