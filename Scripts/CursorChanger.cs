using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    //Member Variables
    CameraRayCaster m_CameraRaycaster;
    [SerializeField] Texture2D m_TargetCursor = null;
    [SerializeField] Texture2D m_WalkCursor = null;
    [SerializeField] Texture2D m_UnknownCursor = null;
    Vector2 m_CursorTargetPoint = new Vector2(96,96);
               
    ////////////////////////////////////////////////////////////////////////
    private void Start()
    {
        m_CameraRaycaster = GetComponent<CameraRayCaster>();
        m_CameraRaycaster.LayerChangeEventBroadcaster += OnLayerChangeEventCallback;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void OnLayerChangeEventCallback(CameraRayCastLayerEnum changedLayer)
    {
        switch (changedLayer)
        {
            case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy:
                Cursor.SetCursor(m_TargetCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Walkable:
                Cursor.SetCursor(m_WalkCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            case CameraRayCastLayerEnum.CameraRayCastLayerEnum_Unknown:
                Cursor.SetCursor(m_UnknownCursor, m_CursorTargetPoint, CursorMode.Auto); break;
            default: break;
        }
    }
}//End Class
