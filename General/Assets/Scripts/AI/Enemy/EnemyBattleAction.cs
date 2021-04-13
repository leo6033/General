using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Enemy/Battle")]
public class EnemyBattleAction : Action
{
    public override void Act(StateController controller)
    {
        Battle(controller);
    }

    private void Battle(StateController controller)
    {
        // 判断是否在攻击范围之内，如果不在则追击，当进入攻击范围开始攻击
        if ((controller.stats.attackRange < Vector3.Distance(controller.transform.position, controller.attackObject.transform.position) && controller.attackObject.tag != "House" && controller.attackObject.tag != "Castle") ||
            ((controller.attackObject.tag == "House" || controller.attackObject.tag == "Castle") && (controller.stats.attackRange + 0.5 < Vector3.Distance(controller.transform.position, controller.attackObject.transform.position))))
        {
            AnimatorStateInfo animatorStateInfo = controller.animator.GetCurrentAnimatorStateInfo(0);
            //if(controller.m_IsRemote)
            //    Debug.Log(Vector3.Distance(controller.transform.position, controller.attackObject.transform.position) + " " + controller.attackObject.tag);
            if (!animatorStateInfo.IsName("battle") || !controller.m_IsRemote)
            {
                controller.navMeshAgent.isStopped = false;
                controller.animator.SetInteger("walk", 2);
                controller.navMeshAgent.SetDestination(controller.attackObject.transform.position);
            }
        }
        else
        {
            //Debug.Log(Vector3.Distance(controller.transform.position, controller.attackObject.transform.position));
            controller.animator.SetInteger("walk", 0);
            controller.navMeshAgent.isStopped = true;
            controller.navMeshAgent.velocity = Vector3.zero;
            controller.attack.AttackEnemy(controller.attackObject, controller.stats.attackRate);
        }
    }
}
