using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransition : PlayersTransition
{
    public override void Enable()
    {
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            NeedTransit = true;
    }
}
