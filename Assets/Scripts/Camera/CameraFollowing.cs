using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private Vector3 _forwardDirection;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    [SerializeField] private float _distance;
    [SerializeField] private float _maxVectorLength = 2;

    private Vector3 _nexPosition;

    private void Start()
    {
        float rotationY = Mathf.Rad2Deg * Mathf.Asin(_forwardDirection.x / _forwardDirection.magnitude);
        transform.rotation = Quaternion.Euler(_angle, rotationY, transform.rotation.eulerAngles.z);
    }

    private void FixedUpdate()
    {
        _nexPosition = _player.position + Vector3.ClampMagnitude(_player.velocity, _maxVectorLength);
        _nexPosition += Vector3.up * Mathf.Cos(Mathf.Deg2Rad * _angle) * _distance;
        _nexPosition += -_forwardDirection * Mathf.Sin(Mathf.Deg2Rad * _angle) * _distance;
        transform.position = Vector3.Lerp(transform.position, _nexPosition, _speed * Time.deltaTime);
    }
}
