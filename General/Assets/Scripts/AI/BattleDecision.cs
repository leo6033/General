using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Battle")]
public class BattleDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool encounter = Battle(controller);
        return encounter;
    }

    private bool Battle(StateController controller)
    {
        return false;
    }
}
