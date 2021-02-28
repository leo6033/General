using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Enemy/Walk")]
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

        //if(controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        //{
        //    controller.pointList.Remove(controller.targetPoint);
        //    float distance = Mathf.Infinity;
        //    foreach(Transform point in controller.pointList)
        //    {
        //        float tmpDistance = Vector3.Distance(controller.transform.position, point.position);
        //        if (tmpDistance < distance)
        //        {
        //            distance = tmpDistance;
        //            controller.targetPoint = point;
        //        }
        //    }
        //}
    }
}
