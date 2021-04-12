using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Player/WalkBattle")]
public class PlayerWalkBattleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool battle = Battle(controller);
        return battle;
    }

    private bool Battle(StateController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.attackObject.transform.position) > controller.stats.attackRange * controller.stats.visionRange * 1.5)
        {
            controller.animator.SetInteger("walk", 2);
            return false;
        }
        else if (!controller.attackObject.GetComponent<Health>().Dead())
            return true;
        controller.animator.SetInteger("walk", 2);
        return false;
    }
}
