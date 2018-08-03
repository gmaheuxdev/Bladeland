using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeaponBehavior : WeaponBehavior
{
    public void OnSwordAttackAnimationFinished()
    {
        m_WeaponOwnerAnimator.SetBool("IsAttacking", false);
        DoSwordWeaponBehavior();
    }

    public void DoSwordWeaponBehavior()
    {
        print("SWORD BEHAVIOR IS GETTING DONE");
    }

    protected override void DoWeaponBehavior()
    {
        throw new NotImplementedException();
    }
}
