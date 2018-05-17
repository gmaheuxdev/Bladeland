using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraCastLayer
{
    //Values must be the same as the editor's layer values
    CameraCastLayer_Walkable = 9,
    CameraCastLayer_Enemy = 10,
    CameraCastLayer_Unknown = -1
 };

///////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////

public class CameraRayCast : MonoBehaviour
{
    //Member variables
    public List<CameraCastLayer> m_CameraCastLayerPriorityList;
    private RaycastHit m_CurrentCameraCastHit;
    private CameraCastLayer m_CurrentCameraCastHitLayer;
    private Camera m_PlayerCamera;
        
    [SerializeField] private float m_CameraCastMaxRange = 100f;

    //Class Events
    public delegate void LayerChangeEvent(CameraCastLayer changedLayer);
    public event LayerChangeEvent LayerChangeEventBroadCaster;
    
////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //GETTERS AND SETTERS
    public float GetCameraCastMaxRange(){return m_CameraCastMaxRange;}
    public RaycastHit GetCurrentCameraCastHit() {return m_CurrentCameraCastHit;}
    public CameraCastLayer GetCurrentCameraHitLayer() {return m_CurrentCameraCastHitLayer;}
    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        //Setup cast priorities
        m_CameraCastLayerPriorityList.Insert(0, CameraCastLayer.CameraCastLayer_Walkable);
        m_CameraCastLayerPriorityList.Insert(0, CameraCastLayer.CameraCastLayer_Enemy);

        m_PlayerCamera = Camera.main;
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update ()
    {
        UpdateCameraCast();
	}

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateCameraCast()
    {
        foreach (CameraCastLayer currentLayerEnum in m_CameraCastLayerPriorityList)
        {
            RaycastHit? PotentialHit = BeginCastForLayer(currentLayerEnum);
            
            if(PotentialHit.HasValue)
            {
               m_CurrentCameraCastHit = PotentialHit.Value;

               if(m_CurrentCameraCastHitLayer != currentLayerEnum)
               {
                   m_CurrentCameraCastHitLayer = currentLayerEnum;
                   LayerChangeEventBroadCaster(m_CurrentCameraCastHitLayer);
               }
               return;
            }
       }

        if(m_CurrentCameraCastHitLayer != CameraCastLayer.CameraCastLayer_Unknown)
        {
            m_CurrentCameraCastHitLayer = CameraCastLayer.CameraCastLayer_Unknown;
            LayerChangeEventBroadCaster(m_CurrentCameraCastHitLayer);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    RaycastHit? BeginCastForLayer(CameraCastLayer layerToFind)
    {
        Ray currentRay = m_PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit currentRayHit;

        bool hasHit = Physics.Raycast(currentRay, out currentRayHit, m_CameraCastMaxRange);

        if(hasHit && currentRayHit.transform.gameObject.layer == (int)layerToFind)
        {
            return currentRayHit;
        }
        return null;
    }


}//End class
