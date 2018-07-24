using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MissileBase
{

    [SerializeField]
    private ParticleSystem explosionPrefab;

    private void Explode()
    {
        ParticleSystem explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }

    protected override void OnTimeEnd()
    {
        Explode();
    }
    protected override void OnImpact(GameObject hitObject)
    {
        Explode();
    }
}
