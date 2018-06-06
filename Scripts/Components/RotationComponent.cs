using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponent : MonoBehaviour
{
    [SerializeField]
    float m_XRotationToApply = 0;
    [SerializeField]
    float m_YRotationToApply = 0;
    [SerializeField]
    float m_ZRotationToApply = 0;

    private void Update()
    {
        transform.Rotate(m_XRotationToApply, m_YRotationToApply, m_ZRotationToApply);
        
    }
}
