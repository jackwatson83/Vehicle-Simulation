using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Traffic/States/State")]
public class State : ScriptableObject
{
    [Tooltip("The actions this state has. These will be looped through in order.")]
    public Action[] actions;
    [Tooltip("These are the decisions the state has, that (depending on the outcome) will move the agent to another state.")]
    public Transition[] transitions;
    [Tooltip("The default gizmo colour.")]
    public Color sceneGizmoColor = Color.grey;

    public void UpdateState(VehicleController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    /// <summary>
    /// This method loops through and performs the actions of this state
    /// </summary>
    /// <param name="controller">The vehicle controller, which functions as the control of the vehicle state machine</param>
    private void DoActions(VehicleController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    /// <summary>
    /// This method checks all of the transitions this state has, and decides if it needs to change to a new state.
    /// </summary>
    /// <param name="controller">The vehicle controller, which functions as the control of the vehicle state machine</param>
    private void CheckTransitions(VehicleController controller)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);
            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
