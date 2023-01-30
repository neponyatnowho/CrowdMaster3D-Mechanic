using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class EnemyStateMachine : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private BrokenState _brokenState;
    
    private HealthContainer _healthContainer;
    private EnemyState _currentState;
    private Rigidbody _objectRigitbody;
    private Collider _objectBoxColider;
    private Animator _objectAnimator;
    private float _minDamage;

    public PlayerStatesMachine Player { get; private set; }

    public event UnityAction<EnemyStateMachine> Died;

    private void OnEnable()
    {
        _healthContainer = GetComponent<HealthContainer>();
        _healthContainer.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _healthContainer.Died -= OnEnemyDied;
    }


    private void Awake()
    {
        _objectAnimator = GetComponent<Animator>();
        _objectRigitbody = GetComponent<Rigidbody>();
        Player = FindObjectOfType<PlayerStatesMachine>();
        _objectBoxColider = GetComponent<BoxCollider>();
        Player.Died += OnPlayerDied;
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(_objectRigitbody, _objectAnimator, Player);
    }

    private void LateUpdate()
    {
        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);

    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
            _currentState.Exid();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_objectRigitbody, _objectAnimator, Player);
    }

    public bool ApplayDamage(Rigidbody rigidbody, float force)
    {
        if(force > _minDamage && _currentState != _brokenState)
        {
            _healthContainer.TakeDamage((int)force);
            Transit(_brokenState);
            _brokenState.ApplayDamage(rigidbody, force);
            return true;
        }
        return false;
    }
    private void OnEnemyDied()
    {
        _objectBoxColider.isTrigger= true;
        enabled = false;
        _objectRigitbody.isKinematic = true;
        Died?.Invoke(this);
    }


    private void OnPlayerDied()
    {
        Player.Died -= OnPlayerDied;
        Player = null;
        Transit(_firstState);
    }
}
