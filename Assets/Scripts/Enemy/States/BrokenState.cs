using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokenState : EnemyState
{
    [SerializeField] private float _fallDistance;
    [SerializeField] private float _forceModifier;

    public event UnityAction Died;

    public void ApplayDamage(Rigidbody attachedBody, float force)
    {
        ObjectAnimator.SetTrigger("fall");
        Vector3 direction = (transform.position - attachedBody.position);
        direction.y = 0;
        ObjectRigidbody.AddForce(direction.normalized * force / _forceModifier, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if(Physics.Raycast(ray, _fallDistance) == false)
        {
            ObjectRigidbody.constraints = RigidbodyConstraints.None;
            Died?.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enabled == false)
            return;

        if (other.TryGetComponent(out IDamageable damageable))
            damageable.ApplayDamage(ObjectRigidbody, ObjectRigidbody.velocity.magnitude);
    }

}
