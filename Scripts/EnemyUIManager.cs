using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    //Member variables
    [SerializeField]Canvas EnemyUICanvasToSpawn;
    Camera m_CachedPlayerCamera;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        m_CachedPlayerCamera = Camera.main;
        Instantiate(EnemyUICanvasToSpawn, transform.position, Quaternion.identity, transform);
    }
        
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        transform.LookAt(m_CachedPlayerCamera.transform);
    }
}
