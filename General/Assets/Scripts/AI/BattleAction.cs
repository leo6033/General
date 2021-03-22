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
        // 判断是否在攻击范围之内，如果不在则追击，当进入攻击范围开始攻击
        if(controller.stats.attackRange < Vector3.Distance(controller.transform.position, controller.attackObject.transform.position) && controller.attackObject.tag != "House" && controller.attackObject.tag != "Castle")
        {
            controller.navMeshAgent.isStopped = false;
            //controller.navMeshAgent.velocity = Vector3.zero;
            controller.navMeshAgent.SetDestination(controller.attackObject.transform.position);
        }
        else if (controller.CheckifCountDownElapsed(controller.stats.attackRate))
        {
            controller.navMeshAgent.isStopped = true;
            controller.navMeshAgent.velocity = Vector3.zero;
            controller.attack.AttackEnemy(controller.attackObject, controller.stats.attackRate);
        }
    }
}
