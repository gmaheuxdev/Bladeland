using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    //Member variables
    private CameraRayCast m_PlayerCameraRaycaster;
    private ThirdPersonCharacter m_PlayerCharacter;
    private Vector3 m_MovementVector;
    private Vector3 m_ClickedPosition;

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        m_PlayerCameraRaycaster = Camera.main.GetComponent<CameraRayCast>();
        m_PlayerCharacter = GetComponent<ThirdPersonCharacter>();
        m_MovementVector = Vector3.zero;
        m_ClickedPosition = transform.position;
    }
    
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (m_PlayerCameraRaycaster.GetCurrentCameraHitLayer() == CameraCastLayer.CameraCastLayer_Walkable)
            {
                m_ClickedPosition = m_PlayerCameraRaycaster.GetCurrentCameraCastHit().point;
                m_MovementVector = m_ClickedPosition - transform.position;
            }
        }

        if (IsMovementRange())
        {
            m_PlayerCharacter.Move(m_MovementVector, false, false);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool IsMovementRange()
    {
        float movementRadius = 0.2f;
        Vector3 playerPosToClickPoint = m_ClickedPosition - transform.position;

        if (playerPosToClickPoint.magnitude > movementRadius)
        {
            return true;
        }

        return false;
    }
}//End class

