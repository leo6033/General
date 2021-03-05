using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float m_StartingHealth = 100;

    private bool m_Dead;
    private float m_CurrentHealth;

    public void TakeDamage(float amount)
    {
        //Debug.Log(this.name + " being attacked, amount: " + amount);
        m_CurrentHealth -= amount;
        if(m_CurrentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
    }

    public void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
    }

    // Update is called once per frame
   public void OnDeath()
    {
        m_Dead = true;

        gameObject.SetActive(false);
    }
}
