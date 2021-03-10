using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public void AttackEnemy(Collider collider, float attackRate)
    {
        Vector3 forward = collider.transform.position - transform.position;
        transform.forward = forward.normalized;
        if(Time.time > nextAttackTime)
        {
            skinnedMeshRenderer.material.color = Color.red;
            Debug.Log(collider.name);
            nextAttackTime = Time.time + attackRate;
            // TODO: 伤害判定等
            Health targetHealth = collider.GetComponent<Health>();
            //Debug.Log(collider.name);
            targetHealth.TakeDamage(30);
            StartCoroutine(ColorReset(attackRate));
        }
    }

    IEnumerator ColorReset(float attackRate)
    {
        yield return new WaitForSeconds(attackRate / 2);
        skinnedMeshRenderer.material.color = Color.black;
    }
}
