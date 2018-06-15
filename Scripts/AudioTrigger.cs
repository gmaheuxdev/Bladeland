using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    AudioSource m_CachedAudioSource;

    void Start()
    {
        m_CachedAudioSource = GetComponent<AudioSource>();    
    }

    void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.layer == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Player && !m_CachedAudioSource.isPlaying)
       {
            m_CachedAudioSource.PlayOneShot(m_CachedAudioSource.clip);
       }
    }
}
