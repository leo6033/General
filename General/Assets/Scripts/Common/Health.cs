using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float m_StartingHealth = 100;

    private bool m_Dead;
    private float m_CurrentHealth;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    public void TakeDamage(float amount)
    {
        if(skinnedMeshRenderer != null)
            skinnedMeshRenderer.material.color = Color.white;
        //Debug.Log(this.name + " being attacked, amount: " + amount);
        m_CurrentHealth -= amount;
        if(m_CurrentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
        StartCoroutine(ColorReset());
    }

    IEnumerator ColorReset()
    {
        yield return new WaitForSeconds(0.2f);
        if (skinnedMeshRenderer != null)
            skinnedMeshRenderer.material.color = Color.black;
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
