using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    //Member variables
    private NavMeshAgent m_CachedPlayerNavMeshAgent;
    private CameraRayCaster m_CachedPlayerCameraRaycaster;
    private ThirdPersonCharacter m_CachedPlayerCharacter;
    private PlayerStatsComponent m_CachedPlayerStatsComponent;
    [SerializeField] int m_ClickManaConsumption;

    //Declarations for Update methods(Prevent declaration every frame)
    private Vector3 m_MovementVector;
    private Vector3 m_ClickedPosition;
    private Vector3 m_MovementDestinationPosition;
    

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        m_CachedPlayerCameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        m_CachedPlayerCharacter = GetComponent<ThirdPersonCharacter>();
        m_MovementVector = Vector3.zero;
        m_ClickedPosition = transform.position;
        m_CachedPlayerNavMeshAgent = GetComponent<NavMeshAgent>();
        m_CachedPlayerStatsComponent = GetComponent<PlayerStatsComponent>();

        m_CachedPlayerNavMeshAgent.updateRotation = false;
        m_CachedPlayerNavMeshAgent.updatePosition = true;
    }
     
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FixedUpdate()
    {
        UpdatePlayerMovement();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdatePlayerMovement()
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

        if (Input.GetMouseButtonDown(1) && m_CachedPlayerCameraRaycaster.GetCurrentSeenLayerEnum() == CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy) //Gonna have to move this out eventually dude
        {
            m_CachedPlayerStatsComponent.RemovePlayerMana(m_ClickManaConsumption);
        }
        if (m_CachedPlayerNavMeshAgent.remainingDistance > m_CachedPlayerNavMeshAgent.stoppingDistance)
        {
            m_CachedPlayerCharacter.Move(m_CachedPlayerNavMeshAgent.desiredVelocity, false, false);
        }
        else
        {
            m_CachedPlayerCharacter.Move(Vector3.zero, false, false);
        }
    }

    //////////////////////////////////////////////////////////////////////////
    
}//End class

