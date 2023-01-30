using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{

    private Vector3 _tapPosition;
    private bool isDirectionChanging;

    public event UnityAction<Vector2> DirectionChanged;
    public event UnityAction PointerUp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _tapPosition = Input.mousePosition;

        if (Input.GetMouseButton(0))
            isDirectionChanging = true;

        if (Input.GetMouseButtonUp(0))
            StopDirectionChange();
    }

    private void FixedUpdate()
    {
        Direction();
    }

    private void Direction()
    {
        if(isDirectionChanging)
            DirectionChanged?.Invoke(Input.mousePosition - _tapPosition);
    }

    private void StopDirectionChange()
    {
        isDirectionChanging = false;
        PointerUp?.Invoke();
    }
}
