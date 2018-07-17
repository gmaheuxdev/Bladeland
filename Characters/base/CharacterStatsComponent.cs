using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsComponent : MonoBehaviour
{
    //Serialized variables
    [SerializeField]private float m_TotalHealth;
    [SerializeField]private float m_CurrentHealth;
    [SerializeField]private float m_TotalMana;
    [SerializeField]private float m_CurrentMana;
    [SerializeField]private float m_ManaRechargeRate;
    [SerializeField]private float m_CritChance;
    [SerializeField]private float m_CritMultiplier;
    [SerializeField]private int m_XPGivenOnDeath;
    //Other member variables
    private int m_CurrentLevel;
    private int m_CurrentlevelXPAmount;
    private int m_XPToLevelUp;
   

    //Getters and Setters
    public float GetTotalHealth() {return m_TotalHealth;}
    public float GetCurrentHealthPercentage() { return m_CurrentHealth / m_TotalHealth;}
    public float GetCurrentHealth() {return m_CurrentHealth;}
    public float GetTotalMana() { return m_TotalMana;}
    public float GetCurrentMana() { return m_CurrentMana;}
    public float GetCurrentManaPercentage() { return m_CurrentMana / m_TotalMana;}
    public float GetCritChance() {return m_CritChance;}
    public float GetCritMultiplier() { return m_CritMultiplier;}
    public int GetXPGivenOnDeath() { return m_XPGivenOnDeath;}

    public void RemoveHealth(float amountToRemove) { m_CurrentHealth -= amountToRemove;}
    public void AddHealth(float amountToAdd) { m_CurrentHealth += amountToAdd;}
    public void RemoveMana(float amountToRemove) { m_CurrentMana -= amountToRemove;}
    public void AddMana(float amountToAdd) { m_CurrentMana += amountToAdd;}
    private void AddXP(int amountToAdd) { m_CurrentlevelXPAmount += amountToAdd;}

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start ()
    {
        m_CurrentHealth = m_TotalHealth;
        m_CurrentMana = m_TotalMana;
	}

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        UpdateManaAmount();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void UpdateManaAmount()
    {
        if (m_CurrentMana != m_TotalMana)
        {
            m_CurrentMana += m_ManaRechargeRate * Time.deltaTime;
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////
    public void OnXpGainCallback(int XPAmount) //Player Only
    {
        m_CurrentlevelXPAmount += XPAmount;
        print("You Gained " + XPAmount + " Experience points");
        print("You now have " + m_CurrentlevelXPAmount + "XP points on this level");
        int XpLeftToLevelUp = m_XPToLevelUp - m_CurrentlevelXPAmount;
        print("You need to gain " + XpLeftToLevelUp + "To Level up");
    }
}
