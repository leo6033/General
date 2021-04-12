using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Player/StandybyBattle")]
public class PlayerStandybyBattleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool battle = Battle(controller);
        return battle;
    }

    private bool Battle(StateController controller)
    {
        // 跑出方格太远，结束攻击状态
        if(Vector3.Distance(controller.transform.position, controller.targetPoint + controller.RelativePosition) > 1)
        {
            controller.animator.SetInteger("walk", 2);
            return false;
        }
        if (!controller.attackObject.GetComponent<Health>().Dead())
            return true;
        controller.animator.SetInteger("walk", 2);
        return false;
    }
}
