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
        controller.navMeshAgent.SetDestination(controller.targetHouse.transform.position + controller.RelativePosition);
        controller.navMeshAgent.isStopped = false;
        controller.animator.SetInteger("walk", 1);

        if (controller.audioSource.clip != controller.WalkAudio)
        {
            controller.audioSource.Stop();
            controller.audioSource.clip = controller.WalkAudio;
            controller.audioSource.loop = true;
            controller.audioSource.Play();
        }

        if ((controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) || controller.targetHouse.GetComponent<Health>().Dead())
        {
            controller.houseList.Remove(controller.targetHouse);
            //Debug.Log(controller.pointList.Count);
            float distance = Mathf.Infinity;
            foreach (GameObject house in controller.houseList)
            {
                float tmpDistance = Vector3.Distance(controller.transform.parent.position, house.transform.position);
                if (tmpDistance < distance)
                {
                    distance = tmpDistance;
                    controller.targetHouse = house;
                }
            }
        }
    }
}
