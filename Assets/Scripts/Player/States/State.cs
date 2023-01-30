using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private PlayersTransition[] _transitions;

    public Rigidbody ObjectRigidbody { get; private set; }
    public Animator ObjectAnimator { get; private set; }

    public void Enter(Rigidbody rigitbody, Animator animator)
    {
        if(enabled == false)
        {
            ObjectRigidbody = rigitbody;
            ObjectAnimator = animator;

            enabled = true;

            SetTransitionEnabled(true);
        }
    }

    public void Exid()
    {
        if(enabled)
            SetTransitionEnabled(false);

        enabled = false;
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if(transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }

    private void SetTransitionEnabled(bool isEnabled)
    {
        foreach (var transition in _transitions)
        {
            transition.enabled = isEnabled;
        }
    }
}
