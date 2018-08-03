using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehavior : MonoBehaviour
{
    //Member variables
    protected WeaponConfig m_WeaponConfig;
    protected GameObject m_WeaponOwner;
    protected Animator m_WeaponOwnerAnimator;
    protected GameObject m_WeaponCurrentTarget;

    //Getters and setters
    public void SetWeaponConfig(WeaponConfig newWeaponConfig) { m_WeaponConfig = newWeaponConfig;}
    public void SetWeaponOwner(GameObject newOwner) { m_WeaponOwner = newOwner;}

    //Abstracts methods
    protected abstract void DoWeaponBehavior();

    //Member methods

    void Start()
    {
        m_WeaponOwnerAnimator = m_WeaponOwner.GetComponent<Animator>();
    }

    public void Use()
    {
        m_WeaponOwnerAnimator.SetBool("IsAttacking", true);

        CameraRayCaster cameraRaycaster = Camera.main.GetComponent<CameraRayCaster>();
        if ((int)cameraRaycaster.GetCurrentSeenLayerEnum() == (int)CameraRayCastLayerEnum.CameraRayCastLayerEnum_Enemy)
        {
           m_WeaponCurrentTarget = cameraRaycaster.GetCurrentActiveHit().collider.gameObject;
        }
    }
}
