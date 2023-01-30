using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    [SerializeField] private EnemyState _targetSate;

    public EnemyState TargetState => _targetSate;
    protected PlayerStatesMachine Player { get; private set; }
    public bool NeedTransit { get; protected set; }

    public void Init(PlayerStatesMachine player)
    {
        Player = player;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}
