using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 9000;
    [SerializeField] private float _baseExplosionRadius = 20;

    public void Explode(Bomb explosionBomb)
    {
        foreach (IPushable target in DefineTargets(explosionBomb))
        {
            Rigidbody explodableObject = target.GetRigidbody();

            explodableObject.AddExplosionForce(_baseExplosionForce, explosionBomb.transform.position, _baseExplosionRadius);
        }
    }

    private List<IPushable> DefineTargets(Bomb explosionBomb)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionBomb.transform.position, _baseExplosionRadius);
        List<IPushable> targets = new();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IPushable target))
            {
                targets.Add(target);
            }
        }

        return targets;
    }
}