using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttackTransition : PlayersTransition
{
    [SerializeField] private AttackState _attackState;

    public override void Enable()
    {
        _attackState.AbilityEnded += OnAbilityEnded;
    }

    private void OnDisable()
    {
        _attackState.AbilityEnded -= OnAbilityEnded;
    }

    private void OnAbilityEnded()
    {
        NeedTransit = true;
    }

    private void Update()
    {
        
    }
}
