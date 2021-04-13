using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Player/Standby")]
public class StandbyAction : Action
{
    public override void Act(StateController controller)
    {
        Standby(controller);
    }

    private void Standby(StateController controller)
    {
        controller.navMeshAgent.SetDestination(controller.targetPoint + controller.RelativePosition);
        controller.navMeshAgent.isStopped = false;

        if (controller.navMeshAgent.remainingDistance > controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) 
        {
            controller.animator.SetInteger("walk", 1);
        }
        else
        {
            controller.navMeshAgent.isStopped = true;
            controller.animator.SetInteger("walk", 0);
        }
            
    }
}
