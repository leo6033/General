using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public List<AudioClip> DeadAudios;

    private float m_StartingHealth;
    private StateController m_StateController;
    private Animator m_Animator;
    private bool m_Dead;
    private float m_CurrentHealth = 100.0f;
    private LegionUtil[] m_LegionUtils;
    private AudioSource audioSource;

    public IEnumerator TakeDamage(float amount, float animationTime, AudioSource audioSource)
    {
        audioSource.Play();
        yield return new WaitForSeconds(animationTime + 0.02f);
        //Debug.Log(this.name + " being attacked, amount: " + amount);
        m_CurrentHealth -= amount;
        if(m_CurrentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
    }

    public void OnEnable()
    {
        m_Animator = GetComponent<Animator>();
        m_StateController = GetComponent<StateController>();
        audioSource = GetComponent<AudioSource>();

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
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = DeadAudios[Random.Range(0, DeadAudios.Count - 1)];
        audioSource.Play();

        gameObject.GetComponent<Collider>().enabled = false;
        decreaseCount();
        Invoke("delete", audioSource.clip.length);
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
    
    public float getHP()
    {
        return m_CurrentHealth;
    }
}
