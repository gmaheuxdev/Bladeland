using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enum values must be the same as the editor's layer values
public enum CameraRayCastLayerEnum
{   
    CameraRayCastLayerEnum_Walkable = 9,
    CameraRayCastLayerEnum_Enemy = 10,
    CameraRayCastLayerEnum_Player = 11,
    CameraRayCastLayerEnum_Unknown = -1
};

///////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////

public class CameraRayCaster : MonoBehaviour
{
    //Member variables
    private Camera m_PlayerCamera;
    private List<CameraRayCastLayerEnum> m_LayerPriorityList;
    [SerializeField]private float m_RaycastMaxRange;
    private CameraRayCastLayerEnum m_CurrentSeenLayerEnum;
    private RaycastHit m_CurrentActivehit;

    //Mouse cursor variables
    [SerializeField] Texture2D m_EnemyCursor = null;
    [SerializeField] Texture2D m_WalkCursor = null;
    [SerializeField] Texture2D m_UnknownCursor = null;
    Vector2 m_CursorTargetPoint = new Vector2(96, 96);

   //Getters and setters
   public CameraRayCastLayerEnum GetCurrentSeenLayerEnum() { return m_CurrentSeenLayerEnum;}
   public RaycastHit GetCurrentActiveHit() { return m_CurrentActivehit;}

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        SetupLayerPriorityList();
        m_PlayerCamera = Camera.main;
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        UpdateCameraRaycast();
    }
        
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateCameraRaycast()
    {
        RaycastHit[] raycastHitList = RayCastMousePosition();
        RaycastHit? priorityHit = FindPriorityHit(raycastHitList);

        if (priorityHit.HasValue)
        {
            ManageMouseCursor(priorityHit);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void SetupLayerPriorityList()
    {
        m_LayerPriorityList = new List<CameraRayCastLayerEnum>(); //Private Array has to be on heap to work..sadly

        //Top priority is the last one inserted at zero.
        m_LayerPriorityList.Insert(0, CameraRayCastLayerEnum.CameraRayCastLayerEnum_Walkable);
        m_LayerPriorityList.Insert(0, CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy);
    }
    
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private RaycastHit[] RayCastMousePosition()
    {
        Ray mousePointerRay = m_PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHitList = Physics.RaycastAll(mousePointerRay, m_RaycastMaxRange);
        Physics.RaycastAll(mousePointerRay);
        return raycastHitList;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    private RaycastHit? FindPriorityHit(RaycastHit[] raycastHitList)
    {
        //Find the highest priority hit
        foreach (CameraRayCastLayerEnum layerToFind in m_LayerPriorityList)
        {
            foreach (RaycastHit currentHit in raycastHitList)
            {
                if ((int)currentHit.transform.gameObject.layer == (int)layerToFind)
                {
                    return currentHit;
                }
            }
        }

        return null;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void ManageMouseCursor(RaycastHit? priorityHit)
    {
        m_CurrentActivehit = priorityHit.Value;
        CameraRayCastLayerEnum foundPriorityLayer = (CameraRayCastLayerEnum)m_CurrentActivehit.transform.gameObject.layer;

        if (foundPriorityLayer != m_CurrentSeenLayerEnum)
        {
            m_CurrentSeenLayerEnum = foundPriorityLayer;
            SetMouseCursor(foundPriorityLayer);
        }
    }
    
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void SetMouseCursor(CameraRayCastLayerEnum layerCursorToApply)
    {
        switch (layerCursorToApply)
        {
            case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy:
                Cursor.SetCursor(m_EnemyCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Walkable:
                Cursor.SetCursor(m_WalkCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Unknown:
                Cursor.SetCursor(m_UnknownCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            default: break;
        }
    }
}
