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

    private bool moving = false;
    public float turnRate = .1f;
    public float moveRate = .01f;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    // Use this for initialization
    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        backgroundSprite = transform.Find("Ship Body").GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            Move();
        }
    }

    public void Shoot(float angle)
    {
        GameObject newMissile = Instantiate(missilePrefab);
        newMissile.GetComponent<Missile>().SetParentShip(this);

        newMissile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        newMissile.transform.position = transform.position;
    }

    public void Move(Vector3 point, Quaternion rotation)
    {
        desiredPosition = point;
        desiredRotation = rotation;
        moving = true;
    }

    private void Move()
    {
        if (!Quaternion.Equals(desiredRotation, transform.rotation))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnRate * Time.fixedDeltaTime);
            // Debug.Log("Rotate");
            // yield return null;
            return;
        }

        if (!Quaternion.Equals(desiredPosition, transform.position))
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveRate * Time.fixedDeltaTime);
            //Debug.Log("Move");
            // yield return null;
            return;
        }

        //Debug.Log("Done");

        moving = false;
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

