using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;

    public void AttackEnemy(Collider collider, float attackRate)
    {
        if(Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate;
            // TODO: 伤害判定等
            Health targetHealth = collider.GetComponent<Health>();
            //Debug.Log(collider.name);
            targetHealth.TakeDamage(30);
        }
    }
}
