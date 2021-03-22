using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Battle")]
public class BattleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool battle = Battle(controller);
        return battle;
    }

    private bool Battle(StateController controller)
    {
        if (controller.attackObject.gameObject.activeSelf)
            return true;
        return false;
    }
}
