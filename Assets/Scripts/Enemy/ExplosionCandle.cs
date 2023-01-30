using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCandle : MonoBehaviour, IDamageable
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private Rigidbody _rigitbody;
    private Collider[] overlappedColiders;

    private void OnEnable()
    {
        _rigitbody = GetComponent<Rigidbody>();
    }
    public bool ApplayDamage(Rigidbody rigidbody, float force)
    {
        Explosion();
        return true;
    }
    private void Explosion()
    {
        overlappedColiders = Physics.OverlapSphere(transform.position, _explosionRadius);

        for (int i = 0; i < overlappedColiders.Length; i++)
        {
            Rigidbody rigitbody = overlappedColiders[i].attachedRigidbody;
            if (rigitbody)
            {
                rigitbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
        Destroy(gameObject);
    }
}
