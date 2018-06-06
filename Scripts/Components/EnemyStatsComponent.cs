using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsComponent : MonoBehaviour
{
    //Member variables
    [SerializeField]private float m_EnemyTotalHealth;
    [SerializeField]private float m_EnemyCurrentHealth;

    //Getters and Setters
    public float GetEnemyTotalHealth() {return m_EnemyTotalHealth;}
    public float GetEnemyCurrentHealth() {return m_EnemyCurrentHealth;}
    public float GetEnemyCurrentHealthPercentage() { return m_EnemyCurrentHealth / m_EnemyTotalHealth;}
    
    public void AddEnemyHealth(float healthToAdd) { m_EnemyTotalHealth += healthToAdd;}
    public void RemoveEnemyHealth(float healthToRemove) { m_EnemyTotalHealth -= healthToRemove;}

    private void Start()
    {
        m_EnemyCurrentHealth = m_EnemyTotalHealth;
    }
}
