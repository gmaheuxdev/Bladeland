using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsComponent : MonoBehaviour
{
    //Member variables
    [SerializeField]private float m_TotalHealth;
    [SerializeField]private float m_CurrentHealth;
    [SerializeField]private float m_TotalMana;
    [SerializeField]private float m_CurrentMana;
    [SerializeField]private float m_ManaRechargeRate;
    [SerializeField]private float m_CritChance;
    [SerializeField]private float m_CritMultiplier;

    //Getters and Setters
    public float GetTotalHealth() {return m_TotalHealth;}
    public float GetCurrentHealthPercentage() { return m_CurrentHealth / m_TotalHealth;}
    public float GetCurrentHealth() {return m_CurrentHealth;}
    public float GetTotalMana() { return m_TotalMana;}
    public float GetCurrentMana() { return m_CurrentMana;}
    public float GetCurrentManaPercentage() { return m_CurrentMana / m_TotalMana;}
    public float GetCritChance() {return m_CritChance;}
    public float GetCritMultiplier() { return m_CritMultiplier;}

    public void RemoveHealth(float amountToRemove) { m_CurrentHealth -= amountToRemove;}
    public void AddHealth(float amountToAdd) { m_CurrentHealth += amountToAdd;}
    public void RemoveMana(float amountToRemove) { m_CurrentMana -= amountToRemove;}
    public void AddMana(float amountToAdd) { m_CurrentMana += amountToAdd;}

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CurrentHealth = m_TotalHealth;
        m_CurrentMana = m_TotalMana;
	}

    void Update()
    {
        if(m_CurrentMana != m_TotalMana)
        {
            m_CurrentMana += m_ManaRechargeRate * Time.deltaTime;
        }
    }
}
