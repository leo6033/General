using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Player/Move")]
public class MoveAction : Action
{
    public State standby;

    public override void Act(StateController controller)
    {
        Move(controller);
    }

    private void Move(StateController controller)
    {
        //Debug.Log(controller.gameObject.name + " Walk" + controller.targetPoint.position + controller.RelativePosition);
        controller.navMeshAgent.SetDestination(controller.targetPoint.position + controller.RelativePosition);
        controller.navMeshAgent.isStopped = false;

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            // 到目标点后，改变状态为 standby
            controller.rigidbody.velocity = Vector3.zero;
            controller.navMeshAgent.isStopped = true;
            controller.TransitionToState(standby);
        }
    }
}
