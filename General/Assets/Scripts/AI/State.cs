using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transifition[] transifitions;

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        for(int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

   private void CheckTransitions(StateController controller)
    {
        for(int i = 0; i < transifitions.Length; i++)
        {
            bool decisionSucceeded = transifitions[i].decision.Decide(controller);

            State transitionState = decisionSucceeded ? transifitions[i].trueState : transifitions[i].falseState;

            controller.TransitionToState(transitionState);

        }
    }
}
