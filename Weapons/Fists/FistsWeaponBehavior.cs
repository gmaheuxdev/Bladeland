using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistsWeaponBehavior : WeaponBehavior
{
    protected override void DoWeaponBehavior()
    {
        print("DOING FISTS BEHAVIOR");
    }

    public void OnHitTargetAnimationEvent()
    {
        print("GRUNT JUST HIT TARGET");
    }
}
