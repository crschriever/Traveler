using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ship : MonoBehaviour
{

    private Ability[] abilities;

    public float aimDistance;

    public LayerMask moveMask;

    private Collider2D myCollider;

    private Sprite backgroundSprite;

    private bool moving = false;
    public float turnRate = .1f;
    public float moveRate = .01f;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;

    private string teamName;

    // Use this for initialization
    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        backgroundSprite = transform.Find("Ship Body").GetComponent<SpriteRenderer>().sprite;

        desiredPosition = transform.position;
        desiredRotation = transform.rotation;

        abilities = GetComponents<Ability>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            Move();
        }
    }

    public void Shoot(float angle, GameObject missilePrefab)
    {
        GameObject newMissile = Instantiate(missilePrefab);
        ShootInstantiatedMissile(angle, newMissile);
    }

    public void ShootInstantiatedMissile(float angle, GameObject missile)
    {
        missile.GetComponent<MissileBase>().SetParentShip(this);

        missile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        missile.transform.position = transform.position;
    }

    public void Move(Vector3 direction, float distance)
    {
        direction.z = 0;
        Move(transform.position + direction.normalized * distance, Quaternion.FromToRotation(Vector3.up, direction));
    }

    public void Move(Vector2 point, Quaternion rotation)
    {
        desiredPosition = point;
        desiredRotation = rotation;
        moving = true;
    }

    private void Move()
    {
        if (!Mathf.Approximately(transform.rotation.eulerAngles.z, desiredRotation.eulerAngles.z))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnRate * Time.fixedDeltaTime);
            // Debug.Log("Rotate");
            // yield return null;
            return;
        }
        else
        {
            transform.rotation = desiredRotation;
        }

        if (!Vector2.Equals(desiredPosition, transform.position))
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

    public bool CanMove(Vector3 direction, float distance)
    {
        RaycastHit2D hit = MoveHit(direction, distance);

        return hit.collider != null;
    }

    public float PossibleMoveDistance(Vector3 direction, float distance)
    {
        RaycastHit2D hit = MoveHit(direction, distance);

        if (hit.collider == null)
        {
            return distance;
        }
        else
        {
            return hit.distance;
        }
    }

    public RaycastHit2D MoveHit(Vector3 direction, float distance)
    {
        direction.z = 0;
        float shipWidth = myCollider.bounds.extents.x / 2;

        Vector3 spread = Vector2.Perpendicular(direction).normalized * shipWidth;

        RaycastHit2D leftHit = Physics2D.Raycast(transform.position - spread, direction, distance, moveMask);

        if (leftHit.collider != null)
        {
            Debug.DrawLine(transform.position - spread, leftHit.point, Color.green, 0, false);
            return leftHit;
        }

        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + spread, direction, distance, moveMask);

        if (rightHit.collider != null)
        {
            Debug.DrawLine(transform.position + spread, rightHit.point, Color.green, 0, false);
        }

        return rightHit;
    }

    public Sprite GetBackgroundSprite()
    {
        return backgroundSprite;
    }

    public Collider2D GetCollider2D()
    {
        return myCollider;
    }

    public bool CanTakeAction()
    {
        return !moving;
    }

    public Vector3 GetDesiredPosition()
    {
        return desiredPosition;
    }

    public Ability GetAbilityOfType(Ability.Type type)
    {
        foreach (Ability ability in abilities)
        {
            if (ability.type == type)
            {
                return ability;
            }
        }

        throw new System.Exception("Ability not found with type: " + type + " in ship: " + gameObject.name);
    }

    public string GetTeamName()
    {
        return teamName;
    }

    public void SetTeamName(string name)
    {
        teamName = name;
    }
}

