using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToCenterOfEnemy : AIBehavior
{

    public float minDistance;
    public float maxDistance;
    private float midDistance;
    public float moveWaitTime;

    public float collisionPadding;

    private float timeSinceLastMove = 0;

    void Start()
    {
        base.Start();
        midDistance = (minDistance + maxDistance) / 2;
    }

    public override bool TakeAction()
    {

        if (timeSinceLastMove > 0)
        {
            return false;
        }

        Vector3 playerShipsMidPoint = BattleManager.instance.FindPlayerShipsMiddle();
        Vector3 displacement = ship.transform.position - playerShipsMidPoint;
        if (Mathf.Abs(displacement.magnitude) < minDistance)
        {
            Debug.Log(displacement.magnitude);
            Debug.Log("Away");

            TryMoveAway(playerShipsMidPoint);
            timeSinceLastMove = moveWaitTime;
        }
        else if (Mathf.Abs(displacement.magnitude) > maxDistance)
        {
            Debug.Log(displacement.magnitude);
            Debug.Log("To");

            TryMoveTowards(playerShipsMidPoint);
            timeSinceLastMove = moveWaitTime;
        }
        else
        {
            return false;
        }

        return true;
    }

    void Update()
    {
        timeSinceLastMove -= Time.deltaTime;
    }

    private void TryMoveAway(Vector3 playerPoint)
    {
        Vector3 direction = ship.transform.position - playerPoint;
        direction.z = 0;
        float distance = direction.magnitude - midDistance;

        // Move as far away as we can from the player
        distance = ship.PossibleMoveDistance(direction, distance);
        distance -= ship.GetCollider2D().bounds.extents.y + collisionPadding;

        ship.Move(direction, distance);
    }

    private void TryMoveTowards(Vector3 playerPoint)
    {
        Vector3 direction = playerPoint - ship.transform.position;
        direction.z = 0;
        float distance = direction.magnitude - midDistance;

        // Move as close as we can to the player
        distance = ship.PossibleMoveDistance(direction, distance);
        distance -= ship.GetCollider2D().bounds.extents.y + collisionPadding;

        ship.Move(direction, distance);
    }

}