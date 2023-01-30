using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private void FixedUpdate()
    {
        ObjectRigidbody.angularVelocity = Vector3.zero;
    }
}
