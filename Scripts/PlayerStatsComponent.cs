using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsComponent : MonoBehaviour
{
    //Member variables
    [SerializeField]private float m_PlayerTotalHealth;
    [SerializeField]private float m_PlayerCurrentHealth;

    //Getters and Setters
    public float GetPlayerTotalHealth() {return m_PlayerTotalHealth;}
    public float GetPlayerCurrentHealthPercentage() { return m_PlayerCurrentHealth / m_PlayerTotalHealth;}
    public float GetPlayerCurrentHealth() {return m_PlayerCurrentHealth;}

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Start ()
    {
        m_PlayerCurrentHealth = m_PlayerTotalHealth;
	}
}
