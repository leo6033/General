using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Player/Encounter")]
public class EncounterDecision : Decision
{
    // 用于玩家 AI 在前往目的地的过程中做决策
    public override bool Decide(StateController controller)
    {
        bool encounter = Encounter(controller);
        return encounter;
    }

    private bool Encounter(StateController controller)
    {
        Collider[] objects = Physics.OverlapSphere(controller.transform.position, controller.stats.attackRange, 1 << 9);
        foreach (Collider c in objects)
        {
            // TODO: 设置攻击目标
            controller.attackObject = c;
            return true;
        }
        return false;
    }
}
