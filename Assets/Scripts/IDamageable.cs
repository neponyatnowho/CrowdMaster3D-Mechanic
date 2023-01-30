using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool ApplayDamage(Rigidbody rigidbody, float force);
}
