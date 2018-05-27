using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    //Member variables
    private RawImage m_HealthBarRawImage;
    private EnemyStatsComponent m_CachedEnemyStatsComponent;
   
    //Update variables
    Rect m_HealthBarNewUVRect;

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_HealthBarRawImage = GetComponent<RawImage>();
        m_CachedEnemyStatsComponent = GetComponentInParent<EnemyStatsComponent>();
        m_HealthBarNewUVRect = m_HealthBarRawImage.uvRect;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        m_HealthBarNewUVRect.x = -(m_CachedEnemyStatsComponent.GetEnemyCurrentHealthPercentage() / 2f) - 0.5f;
        m_HealthBarRawImage.uvRect = m_HealthBarNewUVRect;
    }

}
