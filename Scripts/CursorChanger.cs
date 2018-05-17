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
        m_CameraRaycast.LayerChangeEventBroadCaster += OnLayerChangeEventCallback;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnLayerChangeEventCallback(CameraCastLayer changedLayer)
    {
        switch (changedLayer)
        {
            case CameraCastLayer.CameraCastLayer_Enemy:
                Cursor.SetCursor(m_TargetCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraCastLayer.CameraCastLayer_Walkable:
                Cursor.SetCursor(m_WalkCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraCastLayer.CameraCastLayer_Unknown:
                Cursor.SetCursor(m_UnknownCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            default: break;
        }
    }
}//End Class
