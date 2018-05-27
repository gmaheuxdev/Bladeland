using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    //Member variables
    private RawImage m_HealthBarRawImage;
    private PlayerStatsComponent m_CachedPlayerStatsComponent;
    private GameObject m_CachedPlayerGameObject;

    //Update variables
    Rect m_HealthBarNewUVRect;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_HealthBarRawImage = GetComponent<RawImage>();
        m_CachedPlayerGameObject = GameObject.FindGameObjectWithTag("Player"); //TODO: Better with a helper instead probably?
        m_CachedPlayerStatsComponent = m_CachedPlayerGameObject.GetComponent<PlayerStatsComponent>();
        m_HealthBarNewUVRect = m_HealthBarRawImage.uvRect;
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update ()
    {
        m_HealthBarNewUVRect.x = -(m_CachedPlayerStatsComponent.GetPlayerCurrentHealthPercentage() /2f) - 0.5f;
        m_HealthBarRawImage.uvRect = m_HealthBarNewUVRect;
    }
}
