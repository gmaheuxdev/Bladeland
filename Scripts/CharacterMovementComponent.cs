using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementComponent : MonoBehaviour
{
    //Member variables
    private NavMeshAgent m_CachedPlayerNavMeshAgent;
    private CameraRayCaster m_CachedPlayerCameraRaycaster;
    private PlayerStatsComponent m_CachedPlayerStatsComponent;
    [SerializeField] int m_ClickManaConsumption;

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


    //Declarations for Update methods(Prevent declaration every frame)
    private Vector3 m_MovementVector;
    private Vector3 m_ClickedPosition;
    private Vector3 m_MovementDestinationPosition;
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        m_CachedPlayerCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_CachedPlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        m_CachedPlayerStatsComponent = GetComponent<PlayerStatsComponent>();

        m_MovementVector = Vector3.zero;
        m_ClickedPosition = transform.position;
        m_CachedPlayerNavMeshAgent.updateRotation = false;
        m_CachedPlayerNavMeshAgent.updatePosition = true;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation; 
    }
     
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FixedUpdate()
    {
        UpdatePlayerMovement();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdatePlayerMovement()
    {
        UpdateMovementDestination();
        ApplyMovement();
    }
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateMovementDestination()
    {
        if (Input.GetMouseButton(0))
        {
            switch (m_CachedPlayerCameraRaycaster.GetCurrentSeenLayerEnum())
            {
                case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Walkable:
                case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy:
                    m_ClickedPosition = m_CachedPlayerCameraRaycaster.GetCurrentActiveHit().point;
                    m_CachedPlayerNavMeshAgent.SetDestination(m_ClickedPosition); break;
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ApplyMovement()
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
    public void Move(Vector3 move)
    {
        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
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

