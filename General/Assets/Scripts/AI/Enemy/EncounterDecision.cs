using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Enemy/Encounter")]
public class EncounterDecision : Decision
{
    // 用于敌人 AI 在前往房子的过程中做决策
    public override bool Decide(StateController controller)
    {
        bool encounter = Encounter(controller);
        return encounter;
    }

    private bool Encounter(StateController controller)
    {
        Collider[] objects = Physics.OverlapSphere(controller.transform.position, controller.stats.attackRange, 8);
        foreach(Collider c in objects)
        {
            if(c.tag == "Player")
            {
                // TODO: 设置攻击目标
                return true;
            }
        }
        return false;
    }
}
