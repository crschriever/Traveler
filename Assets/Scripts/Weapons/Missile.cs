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

    [SerializeField]
    private ParticleSystem explosionPrefab;

    private float timeLeft;

    private bool initialParentCollide = true;

    // Use this for initialization
    void Start()
    {
        timeLeft = LIFE_TIME;
        rigidbody = GetComponent<Rigidbody2D>();
        gravity = new GravityEffected(transform, rigidbody);

        rigidbody.velocity = STARTING_VELOCITY * transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            Explode();
        }
    }

    void FixedUpdate()
    {
        gravity.FixedUpdate();
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

        Debug.Log(other.gameObject.name);

        if (other.tag == "Inanimate" || other.tag == "Ship")
        {
            Explode();
        }
    }

    private void Explode()
    {
        ParticleSystem explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
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
