using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    //Member variables
    [SerializeField] private RawImage m_HealthBarRawImage;
    [SerializeField] private RawImage m_ManaBarRawImage;
    private PlayerStatsComponent m_CachedPlayerStatsComponent;
    private GameObject m_CachedPlayerGameObject;


    //Update variables
    Rect m_HealthBarNewUVRect;
    Rect m_ManaBarNewUVRect;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player"); //TODO: Better with a helper instead probably?
        m_CachedPlayerStatsComponent = m_CachedPlayerGameObject.GetComponent<PlayerStatsComponent>();
        m_HealthBarNewUVRect = m_HealthBarRawImage.uvRect;
        m_ManaBarNewUVRect = m_ManaBarRawImage.uvRect;
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update ()
    {
        m_HealthBarNewUVRect.x = -(m_CachedPlayerStatsComponent.GetPlayerCurrentHealthPercentage() /2f) - 0.5f;
        m_HealthBarRawImage.uvRect = m_HealthBarNewUVRect;

        m_ManaBarNewUVRect.x = -(m_CachedPlayerStatsComponent.GetPlayerCurrentManaPercentage() / 2f) - 0.5f;
        m_ManaBarRawImage.uvRect = m_ManaBarNewUVRect;
    }
}
