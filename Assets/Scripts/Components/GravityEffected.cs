using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEffected
{
    private Transform transform;
    private Rigidbody2D rigidbody;

    public GravityEffected(Transform transform, Rigidbody2D rigidbody)
    {
        this.transform = transform;
        Debug.Log(transform.position);
        this.rigidbody = rigidbody;
    }

    public void Update()
    {
        foreach (GameObject gravitySource in BattleManager.instance.gravitySources)
        {
            Vector2 distance = gravitySource.transform.position - transform.position;
            Vector2 direction = distance.normalized;
            float distanceMagnitude = distance.magnitude;

            float masses = rigidbody.mass * gravitySource.GetComponent<Rigidbody2D>().mass;
            float inverseDistanceSquared = Mathf.Pow(distanceMagnitude, -2);

            float force = BattleManager.G * masses * inverseDistanceSquared;

            Debug.Log(transform.position);
            rigidbody.AddForce(force * Time.deltaTime * direction);
        }
    }
}
