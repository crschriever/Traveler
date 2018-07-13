using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public int STARTING_VELOCITY = 7;
    public int LIFE_TIME = 3;

    private Ship parentShip;

    private Rigidbody2D rigidbody;

    private GravityEffected gravity;

    private float timeLeft;

    // Use this for initialization
    void Start()
    {
        timeLeft = LIFE_TIME;
        rigidbody = GetComponent<Rigidbody2D>();
        gravity = new GravityEffected(transform, rigidbody);

        rigidbody.velocity = STARTING_VELOCITY * (transform.rotation * Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            Explode();
        }

        gravity.Update();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
        StateMachine.instance.MissileExploded(this);
    }

    public Ship GetParentShip()
    {
        return parentShip;
    }

    public void SetParentShip(Ship ship)
    {
        parentShip = ship;
    }
}
