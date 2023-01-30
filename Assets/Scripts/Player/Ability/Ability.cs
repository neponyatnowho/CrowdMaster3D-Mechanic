using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : ScriptableObject
{
    protected Rigidbody PlayerRigitbody;

    public abstract event UnityAction AbilityEnded;

    public void Init(Rigidbody rigitbody)
    {
        PlayerRigitbody = rigitbody;
    }

    public abstract void UseAbility(AttackState attack);
}
