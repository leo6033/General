using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public GameObject Bullet;
    public Transform BulletCreateTransform;

    private bool isBattle = false;

    public void AttackEnemy(Collider collider, float attackRate)
    {
        Vector3 forward = collider.transform.position - transform.position;
        transform.forward = forward.normalized;
        if(Time.time > nextAttackTime)
        {
            //skinnedMeshRenderer.material.color = Color.red;
            Animator animator = GetComponent<Animator>();
            //Debug.Log(collider.name);

            // TODO: 伤害判定等
            Health targetHealth = collider.GetComponent<Health>();
            
            if (targetHealth.isActiveAndEnabled)
            {
                if (!isBattle)
                {
                    animator.SetTrigger("battle");
                    isBattle = true;
                }
                
                AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (animatorStateInfo.IsName("battle"))
                {
                    StateController stateController = GetComponent<StateController>();

                    if (stateController.audioSource.isPlaying)
                    {
                        stateController.audioSource.loop = false;
                        stateController.audioSource.Stop();
                    }
                    
                    if(stateController.m_IsRemote)
                    {
                        stateController.audioSource.clip = stateController.AttackAudios[Random.Range(0, stateController.AttackAudios.Count - 1)];
                        StartCoroutine(CreateBullet(collider, animatorStateInfo.length, stateController.audioSource));
                    }
                    else
                    {
                        stateController.audioSource.clip = stateController.AttackAudios[Random.Range(0, stateController.AttackAudios.Count - 1)];
                        StartCoroutine(targetHealth.TakeDamage(CalculateDamage(collider), animatorStateInfo.length, stateController.audioSource));
                    }
                    nextAttackTime = Time.time + attackRate;
                    isBattle = false;
                }
                else if(Time.time > nextAttackTime + attackRate)
                {
                    isBattle = false;
                }
                
            }
            //Debug.Log(collider.name);
            //StartCoroutine(ColorReset(attackRate));
        }
    }

    private float CalculateDamage(Collider collider)
    {
        Stats myStats = GetComponent<StateController>().stats;
        if (collider.tag == "House" || collider.tag == "Castle")
            return myStats.physicalAttack + myStats.magicAttack;
        Stats enemyStats = collider.GetComponent<StateController>().stats;

        float physical = myStats.physicalAttack - enemyStats.physicalDefense;
        physical = physical > 0 ? physical : 0;
        float magic = myStats.magicAttack - enemyStats.magicDefense;
        magic = magic > 0 ? magic : 0;
        float harm = physical + magic;

        int X = Random.Range(0, 100);

        if (X <= enemyStats.dodgeRate)
            harm = 0;
        else if (X <= myStats.critRate + enemyStats.dodgeRate)
            harm =  harm * myStats.critHarm;
        Debug.Log(harm);
        return harm;
    }

    IEnumerator CreateBullet(Collider collider, float animationTime, AudioSource audioSource)
    {
        yield return new WaitForSeconds(animationTime - 1f);
        audioSource.Play();
        GameObject bullet = Instantiate(Bullet, BulletCreateTransform.position, BulletCreateTransform.rotation) as GameObject;
        Bullet b = bullet.GetComponent<Bullet>();
        b.tag = tag;
        b.stats = GetComponent<StateController>().stats;
        StartCoroutine(b.shoot(collider.transform.position));
        
    }

    IEnumerator ColorReset(float attackRate)
    {
        yield return new WaitForSeconds(attackRate / 2);
        skinnedMeshRenderer.material.color = Color.black;
    }
}
