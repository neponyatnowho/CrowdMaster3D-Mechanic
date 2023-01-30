using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private GameObject _joystick;
    [SerializeField] private Transform _joystickDot;
    [SerializeField] private float _maxLength;

    private Vector3 _lastClickPosition;

    private void Update()
    {
        SetEnabled();
        var destination = Input.mousePosition - _lastClickPosition;
        _joystickDot.position = _joystick.transform.position + Vector3.ClampMagnitude(destination, _maxLength);
    }

    private void SetEnabled()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastClickPosition = Input.mousePosition;
            _joystick.transform.position = _lastClickPosition;
            _joystick.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _joystick.SetActive(false);
        }
    }
}
