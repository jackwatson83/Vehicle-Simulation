using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    [Tooltip("The decision this transition is for.")]
    public Decision decision;
    [Tooltip("The state to transition to if the decision returns TRUE.")]
    public State trueState;
    [Tooltip("The state to transition to if the decision returns FALSE.")]
    public State falseState;
}
