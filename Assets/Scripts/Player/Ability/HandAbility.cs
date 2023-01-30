using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "New Hand Ability", menuName = "Player/Abilities/Hand", order = 51)]

public class HandAbility : Ability
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _usefulTime;

    private AttackState _state;
    private Coroutine _corutine;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attack)
    {
        if (_corutine != null)
            Reset();

        _state = attack;

        _corutine = _state.StartCoroutine(Attack(_state));

        _state.CollisionDetected += OnPlayerAttack;
    }

    private void OnPlayerAttack(IDamageable damageable)
    {
        if (damageable.ApplayDamage(_state.ObjectRigidbody, _attackForce) == false)
            return;
        _state.ObjectRigidbody.velocity /= 2f;
    }

    private void Reset()
    {
        _state.ObjectRigidbody.velocity = Vector3.zero;
        _state.StopCoroutine(_corutine);
        _corutine = null;
        _state.CollisionDetected -= OnPlayerAttack;
    }

    private IEnumerator Attack(AttackState state)
    {
        float time = _usefulTime;
        while (time > 0)
        {
            state.ObjectRigidbody.velocity = state.ObjectRigidbody.velocity.normalized * _attackForce;
            time -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        Reset();
        AbilityEnded?.Invoke();
    }
}
