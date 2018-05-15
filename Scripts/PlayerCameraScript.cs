using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    GameObject playerReference;

	void Start ()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
    }
	
    void LateUpdate()
    {
        transform.position = playerReference.transform.position;
    }
}
