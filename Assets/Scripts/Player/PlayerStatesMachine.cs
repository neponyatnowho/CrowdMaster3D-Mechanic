using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(HealthContainer))]
public class PlayerStatesMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    private Rigidbody _objectRigitbody;
    private Animator _objectAnimator;
    private HealthContainer _healthContainer;

    public UnityAction Damaged;

    public event UnityAction Died;

    private void OnEnable()
    {
        _healthContainer.Died += OnDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnDied;
    }

    private void Awake()
    {
        _objectAnimator = GetComponent<Animator>();
        _objectRigitbody = GetComponent<Rigidbody>();
        _healthContainer = GetComponent<HealthContainer>();
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(_objectRigitbody, _objectAnimator);

    }

    private void LateUpdate()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);

    }


    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exid();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_objectRigitbody, _objectAnimator);
    }

    public void ApplyDamage(float damage)
    {
        _healthContainer.TakeDamage((int)damage);
        Damaged?.Invoke();
    }

    private void OnDied()
    {
        _objectAnimator.SetTrigger("broken");
        Died?.Invoke();
        _currentState = _firstState;
        enabled = false;
    }
}
