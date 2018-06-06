using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsComponent : MonoBehaviour
{
    //Member variables
    [SerializeField]private float m_PlayerTotalHealth;
    [SerializeField]private float m_PlayerCurrentHealth;
    [SerializeField]private float m_PlayerTotalMana;
    [SerializeField]private float m_PlayerCurrentMana;

    //Getters and Setters
    public float GetPlayerTotalHealth() {return m_PlayerTotalHealth;}
    public float GetPlayerCurrentHealthPercentage() { return m_PlayerCurrentHealth / m_PlayerTotalHealth;}
    public float GetPlayerCurrentHealth() {return m_PlayerCurrentHealth;}
    public float GetPlayerTotalMana() { return m_PlayerTotalMana;}
    public float GetPlayerCurrentMana() { return m_PlayerCurrentMana;}
    public float GetPlayerCurrentManaPercentage() { return m_PlayerCurrentMana / m_PlayerTotalMana;}

    public void RemovePlayerHealth(float amountToRemove) { m_PlayerCurrentHealth -= amountToRemove;}
    public void AddPlayerHealth(float amountToAdd) { m_PlayerCurrentHealth += amountToAdd;}
    public void RemovePlayerMana(float amountToRemove) { m_PlayerCurrentMana -= amountToRemove;}
    public void AddPlayerMana(float amountToAdd) { m_PlayerCurrentMana += amountToAdd;}

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start ()
    {
        m_PlayerCurrentHealth = m_PlayerTotalHealth;
        m_PlayerCurrentMana = m_PlayerTotalMana;
	}
}
