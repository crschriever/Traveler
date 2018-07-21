using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffected
{

    const float RADIUS_POWER = 2.0f;

    private Transform transform;
    private Rigidbody2D rigidbody;

    public GravityEffected(Transform transform, Rigidbody2D rigidbody)
    {
        this.transform = transform;
        this.rigidbody = rigidbody;
    }

    public void FixedUpdate()
    {
        foreach (GameObject gravitySource in BattleManager.instance.gravitySources)
        {
            Vector2 distance = gravitySource.transform.position - transform.position;
            Vector2 direction = distance.normalized;
            float distanceMagnitude = distance.magnitude;

            float masses = rigidbody.mass * gravitySource.GetComponent<GravitySource>().mass;
            float inverseDistanceSquared = Mathf.Pow(distanceMagnitude, -RADIUS_POWER);

            float force = BattleManager.G * masses * inverseDistanceSquared;

            rigidbody.AddForce(force * Time.fixedDeltaTime * direction);
        }
    }
}
