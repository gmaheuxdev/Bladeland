using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    //Member Variables
    CameraRayCast m_CameraRaycast;
    [SerializeField] Texture2D m_TargetCursor = null;
    [SerializeField] Texture2D m_WalkCursor = null;
    [SerializeField] Texture2D m_UnknownCursor = null;
    Vector2 m_CursorTargetPoint = new Vector2(96,96);

    ////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        m_CameraRaycast = GetComponent<CameraRayCast>();
    }

    ///////////////////////////////////////////////////////////////////////
    private void Update()
    {
        //Change cursor according to what's pointed at
        switch(m_CameraRaycast.GetCurrentCameraHitLayer())
        {
            case CameraCastLayer.CameraCastLayer_Enemy:
                Cursor.SetCursor(m_TargetCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraCastLayer.CameraCastLayer_Walkable:
                Cursor.SetCursor(m_WalkCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraCastLayer.CameraCastLayer_None:
                Cursor.SetCursor(m_UnknownCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            default: break;
        }
    }
}
