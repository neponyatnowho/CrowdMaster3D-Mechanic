using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayersTransition : MonoBehaviour
{
    [SerializeField] private State _targetSatte;

    public State TargetState => _targetSatte;
    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
        Enable();
    }

    public abstract void Enable();

}
