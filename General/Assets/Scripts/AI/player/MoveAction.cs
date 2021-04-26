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
        controller.navMeshAgent.SetDestination(controller.targetPoint + controller.RelativePosition);
        controller.navMeshAgent.isStopped = false;
        controller.animator.SetInteger("walk", 1);

        if (controller.audioSource.clip != controller.WalkAudio)
        {
            controller.audioSource.Stop();
            controller.audioSource.clip = controller.WalkAudio;
            controller.audioSource.loop = true;
            controller.audioSource.Play();
        }

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {

            controller.audioSource.loop = false;
            controller.audioSource.Stop();
            // 到目标点后，改变状态为 standby
            controller.navMeshAgent.isStopped = true;
            controller.animator.SetInteger("walk", 0);
            controller.TransitionToState(standby);
        }
    }
}
