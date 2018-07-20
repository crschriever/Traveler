using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ship : MonoBehaviour
{
    public GameObject missilePrefab;
    public float aimDistance;

    public LayerMask moveMask;

    private Collider2D myCollider;

    private Sprite backgroundSprite;

    // Use this for initialization
    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        backgroundSprite = transform.Find("Ship Body").GetComponent<SpriteRenderer>().sprite;
        Debug.Log(backgroundSprite);
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

    public void Move(Vector3 point, Quaternion rotation)
    {
        transform.position = point;
        transform.rotation = rotation;
    }

    public float FindAimAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }

    public bool DetectHit(Vector3 direction, float distance)
    {
        direction.z = 0;
        float shipWidth = myCollider.bounds.extents.x / 2;

        Vector3 spread = Vector2.Perpendicular(direction).normalized * shipWidth;

        RaycastHit2D leftHit = Physics2D.Raycast(transform.position - spread, direction, distance, moveMask);

        if (leftHit.collider != null)
        {
            Debug.DrawLine(transform.position - spread, leftHit.point, Color.green, 0, false);
            return true;
        }

        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + spread, direction, distance, moveMask);

        if (rightHit.collider != null)
        {
            Debug.DrawLine(transform.position + spread, rightHit.point, Color.green, 0, false);
            return true;
        }

        return false;
    }

    public Sprite GetBackgroundSprite()
    {
        return backgroundSprite;
    }

    public Collider2D GetCollider2D()
    {
        return myCollider;
    }
}

