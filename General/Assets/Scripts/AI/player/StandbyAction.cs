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
        controller.animator.SetInteger("walk", 0);
    }
}
