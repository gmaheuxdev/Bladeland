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

    //Class events
    public delegate void LayerChangeEvent(CameraRayCastLayerEnum changedLayer);
    public event LayerChangeEvent LayerChangeEventBroadcaster;

    //Getters and setters
   public CameraRayCastLayerEnum GetCurrentSeenLayerEnum() { return m_CurrentSeenLayerEnum;}
   public RaycastHit GetCurrentActiveHit() { return m_CurrentActivehit;}

    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        SetupLayerPriorityList();
        m_PlayerCamera = Camera.main;
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

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        Ray mousePointerRay = m_PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] raycastHitList = Physics.RaycastAll(mousePointerRay, m_RaycastMaxRange);
        Physics.RaycastAll(mousePointerRay);

       RaycastHit? foundHit = FindPriorityHit(raycastHitList);

        if(foundHit.HasValue)
        {
            m_CurrentActivehit = foundHit.Value;
            CameraRayCastLayerEnum foundPriorityLayer = (CameraRayCastLayerEnum)m_CurrentActivehit.transform.gameObject.layer;

            if (foundPriorityLayer != m_CurrentSeenLayerEnum)
            {
                m_CurrentSeenLayerEnum = foundPriorityLayer;
                LayerChangeEventBroadcaster(m_CurrentSeenLayerEnum);
            }
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
}
