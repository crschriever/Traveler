using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject missilePrefab;
    public float aimDistance;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Shoot(float angle)
    {
        GameObject newMissile = Instantiate(missilePrefab);
        newMissile.GetComponent<Missile>().SetParentShip(this);

        newMissile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 direction = newMissile.transform.rotation * Vector2.up;

        // Don't account for z when normalizing
        direction.z = 0;

        newMissile.transform.position = transform.position + (direction.normalized * aimDistance * transform.localScale.magnitude);
    }

    public void Move(Vector3 point)
    {
        //transform.Translate
    }
}

