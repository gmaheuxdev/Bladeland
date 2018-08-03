using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbiltyComponent : MonoBehaviour
{
    [SerializeField] SpecialAbilityConfig[] m_PlayerSpecialAbilities;

    void Start()
    {
        SetupSpecialAbilities();
    }

    ////////////////////////////////////////////////////////////////////////
    private void SetupSpecialAbilities()
    {
        for (int i = 0; i < m_PlayerSpecialAbilities.Length; i++)
        {
            m_PlayerSpecialAbilities[i].SetupAbility(gameObject);
        }
    }

    
    ////////////////////////////////////////////////////////////////////////
    public void UseSpecialAbility(int abilityIndex)
    {
        if(m_PlayerSpecialAbilities.Length >=1)
        {
            m_PlayerSpecialAbilities[abilityIndex].UseSpecialAbility();
        }
    }
}
