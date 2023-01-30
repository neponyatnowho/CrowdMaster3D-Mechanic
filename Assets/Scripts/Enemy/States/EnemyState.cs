using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    [SerializeField] private EnemyTransition[] _transitions;

    public Rigidbody ObjectRigidbody { get; private set; }

    public PlayerStatesMachine Player { get; private set; }
    public Animator ObjectAnimator { get; private set; }

    public void Enter(Rigidbody rigitbody, Animator animator, PlayerStatesMachine player)
    {
        if (!enabled)
        {
            ObjectRigidbody = rigitbody;
            ObjectAnimator = animator;
            Player = player;

            enabled = true;

            SetTransitionEnabled(true);
        }
    }

    public void Exid()
    {
        if (enabled)
            SetTransitionEnabled(false);
        
        enabled = false;
    }

    public EnemyState GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }

    private void SetTransitionEnabled(bool isEnabled)
    {
        foreach (var transition in _transitions)
        {
            transition.enabled = isEnabled;
            if(isEnabled)
                transition.Init(Player);
        }
    }
}
