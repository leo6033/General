using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Battle")]
public class BattleAction : Action
{
    public override void Act(StateController controller)
    {
        Battle(controller);
    }

    private void Battle(StateController controller)
    {
        controller.navMeshAgent.isStopped = true;
        if (controller.CheckifCountDownElapsed(controller.stats.attackRate))
        {
            controller.attack.AttackEnemy(controller.attackObject, controller.stats.attackRate);
        }
    }
}
