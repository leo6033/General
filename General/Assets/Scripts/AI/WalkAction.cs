using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Walk")]
public class WalkAction : Action
{
    public override void Act(StateController controller)
    {
        Walk(controller);
    }

    private void Walk(StateController controller)
    {
        controller.navMeshAgent.SetDestination(controller.targetPoint.position);
        controller.navMeshAgent.isStopped = false;
    }
}
