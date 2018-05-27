using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    //Member variables
    private CameraRayCast m_CachedPlayerCameraRaycaster;
    private ThirdPersonCharacter m_CachedPlayerCharacter;
    [SerializeField] float m_MovementStopDistance;

    //Declarations for Update methods(Prevent declaration every frame)
    private Vector3 m_MovementVector;
    private Vector3 m_ClickedPosition;
    private Vector3 m_MovementDestinationPosition;

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        m_CachedPlayerCameraRaycaster = Camera.main.GetComponent<CameraRayCast>();
        m_CachedPlayerCharacter = GetComponent<ThirdPersonCharacter>();
        m_MovementVector = Vector3.zero;
        m_ClickedPosition = transform.position;
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
            switch(m_CachedPlayerCameraRaycaster.GetCurrentCameraHitLayer())
            {
                case CameraCastLayer.CameraCastLayer_Walkable:
                case CameraCastLayer.CameraCastLayer_Enemy:
                    m_ClickedPosition = m_CachedPlayerCameraRaycaster.GetCurrentCameraCastHit().point;
                    m_MovementVector = m_ClickedPosition - transform.position;
                    m_MovementDestinationPosition = CalculateMovementDestinationPosition(); break;
            }
        }

        if (IsMovementRange())
        {
            m_CachedPlayerCharacter.Move(m_MovementVector, false, false);
        }
        else
        {
            StopPlayerMovement();
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool IsMovementRange()
    {
        Vector3 playerPosToClickPoint = transform.position - m_ClickedPosition;
        
        if (playerPosToClickPoint.magnitude > m_MovementStopDistance)
        {
            return true;
        }
     
        return false;
    }
        
    ///////////////////////////////////////////////////////////////////////////
    void StopPlayerMovement()
    {
        m_MovementVector = Vector3.zero;
        m_CachedPlayerCharacter.Move(m_MovementVector, false, false);
    }

    //////////////////////////////////////////////////////////////////////////
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, m_ClickedPosition);
        Gizmos.DrawSphere(m_ClickedPosition, 0.1f);
        Gizmos.DrawSphere(m_MovementDestinationPosition, 0.1f);
    }

   //////////////////////////////////////////////////////////////////////////////
    Vector3 CalculateMovementDestinationPosition()
    {
        Vector3 movementDestinationReduction = ((m_ClickedPosition - transform.position).normalized) * m_MovementStopDistance;
        return m_ClickedPosition - movementDestinationReduction;
    }

}//End class

