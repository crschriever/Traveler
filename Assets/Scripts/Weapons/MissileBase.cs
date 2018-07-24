using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissileBase : MonoBehaviour
{

    public int startingVelocity = 10;
    public int minVelocity = 6;
    public float noEffectTime = .1f;
    public int lifeTime = 6;

    public float dontHitParentTime = .1f;

    protected Ship parentShip;

    private Rigidbody2D rigidbody;

    private GravityEffected gravity;

    private float timeLeft;
    private float dontHitParentTimeLeft;
    private float noEffectTimeLeft;

    private bool initialParentCollide = true;

    // Use this for initialization
    void Start()
    {
        timeLeft = lifeTime;
        dontHitParentTimeLeft = dontHitParentTime;
        noEffectTimeLeft = noEffectTime;
        rigidbody = GetComponent<Rigidbody2D>();
        gravity = new GravityEffected(transform, rigidbody);

        rigidbody.velocity = startingVelocity * transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        dontHitParentTimeLeft -= Time.deltaTime;
        noEffectTimeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            OnTimeEnd();
        }
    }

    void FixedUpdate()
    {
        if (noEffectTimeLeft <= 0)
        {
            gravity.FixedUpdate();
            if (Mathf.Abs(rigidbody.velocity.magnitude) < minVelocity)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * minVelocity;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When being launched, we start inside parent ship. Don't explode then.
        if (other.gameObject == parentShip.gameObject && initialParentCollide)
        {
            return;
        }
        else
        {
            initialParentCollide = false;
        }

        // When being launched make sure we don't splode the parent ship
        if (other.gameObject == parentShip.gameObject && dontHitParentTimeLeft > 0)
        {
            return;
        }

        if (other.tag == "Inanimate" || other.tag == "Ship")
        {
            // Debug.Log(other.gameObject);
            OnImpact(other.gameObject);
        }
    }

    public Ship GetParentShip()
    {
        return parentShip;
    }

    public void SetParentShip(Ship ship)
    {
        parentShip = ship;
    }

    protected abstract void OnTimeEnd();
    protected abstract void OnImpact(GameObject hitObject);
}
