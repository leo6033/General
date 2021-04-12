using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public GameObject Bullet;

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
                animator.SetTrigger("battle");
                AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (animatorStateInfo.IsName("battle"))
                {
                    StateController stateController = GetComponent<StateController>();
                    if(stateController.m_IsRemote)
                    {
                        StartCoroutine(CreateBullet(collider, animatorStateInfo.length));
                    }
                    else
                    {
                        StartCoroutine(targetHealth.TakeDamage(CalculateDamage(collider), animatorStateInfo.length));
                    }
                    nextAttackTime = Time.time + attackRate;
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
            return 0;
        else if (X <= myStats.critRate + enemyStats.dodgeRate)
            return harm * myStats.critHarm;

        return harm;
    }

    IEnumerator CreateBullet(Collider collider, float animationTime)
    {
        yield return new WaitForSeconds(animationTime - 0.02f);

    }

    IEnumerator ColorReset(float attackRate)
    {
        yield return new WaitForSeconds(attackRate / 2);
        skinnedMeshRenderer.material.color = Color.black;
    }
}
