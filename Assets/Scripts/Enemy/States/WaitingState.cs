using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : EnemyState
{
    private void OnEnable()
    {
        ObjectAnimator.SetTrigger("setup");
    }

    private void OnDisable()
    {
        ObjectAnimator.ResetTrigger("setup");

    }

    private void Update()
    {
        
    }
}
