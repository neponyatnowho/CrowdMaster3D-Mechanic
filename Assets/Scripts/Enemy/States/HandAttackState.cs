using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttackState : EnemyState
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackDelay;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Attack());
    }


    private IEnumerator Attack()
    {
        while(enabled)
        {
            transform.LookAt(Player.transform);
            ObjectAnimator.SetTrigger("attack");
            yield return new WaitForSeconds(_attackDelay);
            Player.ApplyDamage(_attackForce);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        ObjectAnimator.ResetTrigger("attack");

    }
}
