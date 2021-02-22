using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private float nextAttackTime;

    public void AttackEnemy(float attackRate)
    {
        if(Time.time > nextAttackTime)
        {
            nextAttackTime = Time.time + attackRate;
            // TODO: 伤害判定等
        }
    }
}
