using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Player/Standby")]
public class StandbyDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool standby = Standby(controller);
        return standby;
    }

    private bool Standby(StateController controller)
    {
        Collider[] objects = Physics.OverlapSphere(controller.transform.position, controller.stats.visionRange, 1 << 9);
        foreach (Collider c in objects)
        {
            // TODO: 设置攻击目标
            controller.attackObject = c;
            return true;
        }
        return false;
    }
}
