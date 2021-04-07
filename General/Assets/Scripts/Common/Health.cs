using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float m_StartingHealth;
    private StateController m_StateController;
    private Animator m_Animator;
    private bool m_Dead;
    private float m_CurrentHealth = 100.0f;
    private LegionUtil[] m_LegionUtils;

    public IEnumerator TakeDamage(float amount, float animationTime)
    {
        yield return new WaitForSeconds(animationTime + 0.02f);
        //Debug.Log(this.name + " being attacked, amount: " + amount);
        m_CurrentHealth -= amount;
        if(m_CurrentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
        //foreach (LegionUtil legion in m_LegionUtils)
        //{
        //    if (legion.getColor() != Color.white)
        //    {
        //        legion.setColor(Color.red);
        //    }
        //    else
        //        break;
        //}
    }

    public void OnEnable()
    {
        m_Animator = GetComponent<Animator>();
        m_StateController = GetComponent<StateController>();
        //if (m_StateController != null)
        //    m_CurrentHealth = m_StateController.stats.HP;
        //else
        //    m_CurrentHealth = 100.0f;
        m_Dead = false;
    }

    // Update is called once per frame
    public void OnDeath()
    {
        m_Dead = true;
        StopAllCoroutines();
        if(m_StateController != null)
            m_StateController.enabled = false;
        if(m_Animator != null)
            m_Animator.SetBool("dead", true);
        gameObject.GetComponent<Collider>().enabled = false;
        decreaseCount();
        Invoke("delete", 2.0f);
    }

    public bool Dead()
    {
        return m_Dead;
    }

    private void delete()
    {
        gameObject.SetActive(false);
    }

    public void Init(float HP)
    {
        m_CurrentHealth = HP;
    }

    private void decreaseCount()
    {
        PlayerManager playerManager = GetComponentInParent<PlayerManager>();
        EnemyManager enemyManager = GetComponentInParent<EnemyManager>();
        if (playerManager != null)
        {
            playerManager.count -= 1;
        }
        else if (enemyManager != null)
            enemyManager.count -= 1;
    }
}
