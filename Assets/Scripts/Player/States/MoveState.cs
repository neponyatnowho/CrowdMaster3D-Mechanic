using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private StaminaAccumulator _staminaAcomulator;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _speedRatio;

    private void OnEnable()
    {
        _playerInput.DirectionChanged += OnDirectionChanged;
        _staminaAcomulator.StartAccumulate();
    }

    private void OnDisable()
    {
        _playerInput.DirectionChanged -= OnDirectionChanged;
        ObjectAnimator.SetFloat("run", 0);
    }

    private void OnDirectionChanged(Vector2 direction)
    {
        ObjectRigidbody.velocity = new Vector3(-direction.y, 0, direction.x) * _speedRatio;
        if(ObjectRigidbody.velocity.magnitude > _maxSpeed)
            ObjectRigidbody.velocity *= _maxSpeed / ObjectRigidbody.velocity.magnitude;
        
        if(ObjectRigidbody.velocity.magnitude != 0)
            ObjectRigidbody.MoveRotation(Quaternion.LookRotation(ObjectRigidbody.velocity, Vector3.up));
    }

    private void Update()
    {
        ObjectAnimator.SetFloat("run", ObjectRigidbody.velocity.magnitude / _maxSpeed);
    }
}
